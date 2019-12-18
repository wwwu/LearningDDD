using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDDD.Domain.Events
{
    public class EventBase
    {
        protected EventBase(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }

        public Guid AggregateId { get; protected set; }

        public DateTime Timestamp { get; private set; } = DateTime.Now;
    }
}
