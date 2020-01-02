using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using LearningDDD.Domain.Commands.User;

namespace LearningDDD.Domain.Validations.User
{
    public class RemoveUserCommandValidator : AbstractValidator<RemoveUserCommand>
    {
        public RemoveUserCommandValidator()
        {
            RuleFor(s => s.Id)
                   .NotEmpty().WithMessage("姓名不能为空");
        }
    }
}
