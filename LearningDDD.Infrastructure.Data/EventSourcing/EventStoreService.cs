using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LearningDDD.Domain.Core.Events;
using LearningDDD.Domain.IRepository;
using Newtonsoft.Json;

namespace LearningDDD.Infrastructure.Data.EventSourcing
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
            var storedEvent = new StoredEvent(@event, json, "w");
            await _storedEventRepository.AddAsync(storedEvent);
            await _unitOfWork.CommitAsync();
        }
    }
}
