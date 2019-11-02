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
        public CreateUserCommand(Models.User user)
        {
            Id = user.Id;
            Password = user.Password;
            Name = user.Name;
            Email = user.Email;
            Address = user.Address;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateUserValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
