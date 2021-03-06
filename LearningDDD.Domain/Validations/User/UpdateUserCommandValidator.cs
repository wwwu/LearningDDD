﻿using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using LearningDDD.Domain.Commands.User;

namespace LearningDDD.Domain.Validations.User
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            //姓名
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("姓名不能为空")
                .Length(2, 10).WithMessage("姓名在2~10个字符之间");

            //地址
            RuleFor(s => s.Address.City)
                .Length(1, 50);
            RuleFor(s => s.Address.Province)
                .Length(1, 50);
            RuleFor(s => s.Address.StreetAndNumber)
                .Length(1, 100);
        }
    }
}
