using System;
using System.Collections.Generic;
using System.Text;
using LearningDDD.Domain.Events;
using MediatR;

namespace LearningDDD.Domain.Commands
{
    /// <summary>
    /// 命令模型基类
    /// </summary>
    public abstract class Command : EventBase, IRequest
    {
        protected Command(Guid aggregateId) : base(aggregateId)
        {
        }

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
