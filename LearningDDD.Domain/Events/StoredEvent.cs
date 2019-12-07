﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDDD.Domain.Events
{
    public class StoredEvent : Event
    {
        public StoredEvent(Event @event, string data, string user)
        {
            AggregateId = @event.AggregateId;
            Data = data;
            User = user;
        }

        protected StoredEvent() { }

        public Guid Id { get; private set; }

        public string Data { get; private set; }

        public string User { get; private set; }
    }
}
