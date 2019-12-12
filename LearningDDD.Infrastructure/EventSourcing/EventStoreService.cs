using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LearningDDD.Domain.Commands;
using LearningDDD.Domain.Events;
using LearningDDD.Domain.IRepository;
using Newtonsoft.Json;

namespace LearningDDD.Infrastructure.EventSourcing
{
    public class EventStoreService : IEventStoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStoredEventRepository _storedEventRepository;

        public EventStoreService(IUnitOfWork unitOfWork,
            IStoredEventRepository storedEventRepository)
        {
            _unitOfWork = unitOfWork;
            _storedEventRepository = storedEventRepository;
        }

        public async Task Save<T>(T @event) where T : Event
        {
            var json = JsonConvert.SerializeObject(@event);
            var storedEvent = new Domain.Models.StoredEvent
            {
                AggregateId = @event.AggregateId,
                Timestamp = @event.Timestamp,
                MessageType = typeof(T).FullName,
                User = "w",
                Data = JsonConvert.SerializeObject(@event)
            };
            await _storedEventRepository.AddAsync(storedEvent);
            await _unitOfWork.CommitAsync();
        }
    }
}
