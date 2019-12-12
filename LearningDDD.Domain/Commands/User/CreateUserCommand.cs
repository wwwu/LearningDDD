using System;
using System.Collections.Generic;
using System.Text;
using LearningDDD.Domain.Validations.User;

namespace LearningDDD.Domain.Commands.User
{
    /// <summary>
    /// 创建User命令模型
    /// </summary>
    public class CreateUserCommand : UserCommand
    {
        public CreateUserCommand(string password, string name, string email
            , string city, string province, string streetAndNumber) : base(Guid.Empty)
        {
            Password = password;
            Name = name;
            Email = email;
            Address = new Models.Address
            {
                City = city,
                Province = province,
                StreetAndNumber = streetAndNumber,
            };
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateUserValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
