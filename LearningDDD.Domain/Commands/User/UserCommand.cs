using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDDD.Domain.Commands.User
{
    /// <summary>
    /// User命令模型基类
    /// </summary>
    public abstract class UserCommand : Command
    {
        protected UserCommand(Guid aggregateId) : base(aggregateId)
        {
        }

        public Guid Id { get; protected set; }

        public string Password { get; protected set; }

        public string Name { get; protected set; }

        public string Email { get; protected set; }

        public Models.Address Address { get; protected set; }
    }
}
