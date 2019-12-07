using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDDD.Domain.Commands.User
{
    public class RemoveUserCommand : Command
    {
        public Guid Id { get; set; }

        public RemoveUserCommand(Guid id)
        {
            Id = id;
        }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
