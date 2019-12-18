using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LearningDDD.Domain.Events;
using LearningDDD.Domain.Models;
using LearningDDD.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace LearningDDD.Infrastructure.EventSourcing
{
    public class EventStoreService : IEventStoreService
    {
        private readonly IConfiguration _configuration;
        private readonly UserContext _dbContext;

        public EventStoreService(IConfiguration configuration)
        {
            //借助数据库做存储，避免与领域层业务内使用同一上下文
            _configuration = configuration;
            var dbContextOptions = new DbContextOptions<UserContext>();
            var dbContextOptionBuilder = new DbContextOptionsBuilder<UserContext>(dbContextOptions)
                .UseMySql(_configuration["ConnectionStrings:DefaultConnection"]);
            _dbContext = new UserContext(dbContextOptionBuilder.Options);
        }

        public Task Save<T>(T @event) where T : EventBase
        {
            var storedEvent = new StoredEvent
            {
                AggregateId = @event.AggregateId,
                Timestamp = @event.Timestamp,
                MessageType = typeof(T).FullName,
                User = string.Empty,
                Data = JsonConvert.SerializeObject(@event)
            };
            _dbContext.Set<StoredEvent>().Add(storedEvent);
            return _dbContext.SaveChangesAsync();
        }
    }
}
