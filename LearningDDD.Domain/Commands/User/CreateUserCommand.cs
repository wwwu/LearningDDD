using System;
using System.Collections.Generic;
using System.Text;
using LearningDDD.Domain.Models.Base;
using LearningDDD.Domain.Validations.User;

namespace LearningDDD.Domain.Commands.User
{
    /// <summary>
    /// 创建User命令模型
    /// </summary>
    public class CreateUserCommand : Command<BaseResult>
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

        public Guid Id { get; protected set; }

        public string Password { get; protected set; }

        public string Name { get; protected set; }

        public string Email { get; protected set; }

        public Models.Address Address { get; protected set; }

        public override bool IsValid()
        {
            ValidationResult = new CreateUserCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
