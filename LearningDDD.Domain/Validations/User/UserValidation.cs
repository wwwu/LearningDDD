using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using FluentValidation;
using LearningDDD.Domain.Commands.User;

namespace LearningDDD.Domain.Validations.User
{
    /// <summary>
    /// User命令模型验证基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UserValidation<T> : AbstractValidator<T> where T : UserCommand
    {
        /// <summary>
        /// 姓名校验
        /// </summary>
        protected void ValidateName()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage("姓名不能为空")
                .Length(2, 10).WithMessage("姓名在2~10个字符之间");
        }

        /// <summary>
        /// 密码校验
        /// </summary>
        protected void ValidatePassword()
        {
            RuleFor(s => s.Password)
                .NotEmpty()
                //.Must(s=> Regex.IsMatch(s, @"^(?![^a-zA-Z]+$)(?!\D+$)")).WithMessage("密码必须为数字加字母组合")
                .Length(6, 20).WithMessage("密码必须6~20个字符之间");
        }

        /// <summary>
        /// Email校验
        /// </summary>
        protected void ValidateEmail()
        {
            RuleFor(s => s.Email)
                .NotEmpty()
                .EmailAddress();
        }

        /// <summary>
        /// Address校验
        /// </summary>
        protected void ValidateAddress()
        {
            RuleFor(s => s.Address.City)
                .Length(1, 50);
            RuleFor(s => s.Address.Province)
                .Length(1, 50);
            RuleFor(s => s.Address.StreetAndNumber)
                .Length(1, 100);
        }
    }
}
