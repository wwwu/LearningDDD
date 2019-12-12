using System;
using System.Collections.Generic;
using System.Text;
using LearningDDD.Domain.Models.Base;

namespace LearningDDD.Domain.Models
{
    public class StoredEvent: BaseEntity
    {
        public string Data { get; set; }

        public string User { get; set; }

        public string MessageType { get; set; }

        public Guid AggregateId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
