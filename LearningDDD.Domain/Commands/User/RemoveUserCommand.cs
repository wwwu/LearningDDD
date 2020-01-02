using System;
using System.Collections.Generic;
using System.Text;
using LearningDDD.Domain.Validations.User;

namespace LearningDDD.Domain.Commands.User
{
    public class RemoveUserCommand : Command<bool>
    {
        public RemoveUserCommand(Guid id) : base(id)
        {
            Id = id;
        }

        public Guid Id { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new RemoveUserCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
