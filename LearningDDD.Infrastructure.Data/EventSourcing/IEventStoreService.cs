using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LearningDDD.Domain.Core.Events;

namespace LearningDDD.Infrastructure.Data.EventSourcing
{
    public interface IEventStoreService
    {
        Task Save<T>(T @event) where T : Event;
    }
}
