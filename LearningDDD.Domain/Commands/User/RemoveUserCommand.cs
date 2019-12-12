using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDDD.Domain.Commands.User
{
    public class RemoveUserCommand : Command
    {
        public RemoveUserCommand(Guid id) : base(id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public override bool IsValid()
        {
            return true;
        }
    }
}
