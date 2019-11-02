using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using LearningDDD.Domain.Commands.User;

namespace LearningDDD.Domain.Validations.User
{
    /// <summary>
    /// 创建User命令模型验证
    /// </summary>
    public class CreateUserValidation : UserValidation<CreateUserCommand>
    {
        public CreateUserValidation()
        {
            ValidateName();
            ValidatePassword();
            ValidateEmail();
            ValidateAddress();

            //额外特有验证
            RuleFor(s => s.Name)
                .Must(s => !s.Contains("fuck")).WithMessage("姓名中不能包含非法字符！");
        }
    }
}
