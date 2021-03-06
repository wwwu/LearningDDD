﻿using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using LearningDDD.Domain.Commands.User;

namespace LearningDDD.Domain.Validations.User
{
    /// <summary>
    /// 创建User命令模型验证
    /// </summary>
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            //姓名
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("姓名不能为空")
                .Length(2, 10).WithMessage("姓名在2~10个字符之间");
            
            //密码
            RuleFor(s => s.Password)
                .NotEmpty()
                //.Must(s=> Regex.IsMatch(s, @"^(?![^a-zA-Z]+$)(?!\D+$)")).WithMessage("密码必须为数字加字母组合")
                .Length(6, 20).WithMessage("密码必须6~20个字符之间");

            //邮箱
            RuleFor(s => s.Email)
                .NotEmpty()
                .EmailAddress();

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
