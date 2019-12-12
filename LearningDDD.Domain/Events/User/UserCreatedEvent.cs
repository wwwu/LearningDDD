using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDDD.Domain.Events.User
{
    public class UserCreatedEvent : Event
    {
        public UserCreatedEvent(Guid id, string name, string email) : base(id)
        {
            AggregateId = id;
            Id = id;
            Name = name;
            Email = email;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Email { get; }
    }
}
