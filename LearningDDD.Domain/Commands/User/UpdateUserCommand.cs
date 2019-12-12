using System;
using System.Collections.Generic;
using System.Text;
using LearningDDD.Domain.Validations.User;

namespace LearningDDD.Domain.Commands.User
{
    public class UpdateUserCommand : UserCommand
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

        public override bool IsValid()
        {
            ValidationResult = new UpdateUserValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
