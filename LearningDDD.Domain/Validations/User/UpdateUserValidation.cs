using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using LearningDDD.Domain.Commands.User;

namespace LearningDDD.Domain.Validations.User
{
    public class UpdateUserValidation : UserValidation<UpdateUserCommand>
    {
        public UpdateUserValidation()
        {
            ValidateName();
            ValidateAddress();

            //额外特有验证
            RuleFor(s => s.Name)
                .Must(s => !s.Contains("fuck")).WithMessage("姓名中不能包含非法字符！");
        }
    }
}
