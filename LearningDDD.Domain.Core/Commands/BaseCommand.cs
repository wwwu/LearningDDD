using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace LearningDDD.Domain.Core.Commands
{
    /// <summary>
    /// 命令模型基类
    /// </summary>
    public abstract class BaseCommand : IRequest
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Timestamp { get; private set; } = DateTime.Now;

        /// <summary>
        /// 模型验证验证结果
        /// </summary>
        public FluentValidation.Results.ValidationResult ValidationResult { get; set; }

        /// <summary>
        /// 抽象方法，是否有效
        /// </summary>
        /// <returns></returns>
        public abstract bool IsValid();
    }
}
