using System;
using System.Collections.Generic;
using System.Text;
using LearningDDD.Domain.Models.Base;
using LearningDDD.Domain.Validations.User;

namespace LearningDDD.Domain.Commands.User
{
    public class UpdateUserCommand : Command<BaseResult>
    {
        public UpdateUserCommand(Guid id, string name, string city, string province, string streetAndNumber) : base(id)
        {
            Id = id;
            Name = name;
            Address = new Models.Address
            {
                City = city,
                Province = province,
                StreetAndNumber = streetAndNumber,
            };
        }

        public Guid Id { get; protected set; }

        public string Name { get; protected set; }

        public Models.Address Address { get; protected set; }

        public override bool IsValid()
        {
            ValidationResult = new UpdateUserCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
