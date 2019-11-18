using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LearningDDD.Domain.Core.Bus;
using LearningDDD.Domain.Core.Commands;
using LearningDDD.Domain.Core.Events;
using MediatR;

namespace LearningDDD.Infrastructure.Data.Bus
{
    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<Unit> SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        /// <summary>
        /// 引发事件的实现方法
        /// </summary>
        /// <typeparam name="T">Event：INotification</typeparam>
        /// <param name="event">Event事件模型</param>
        /// <returns></returns>
        public Task RaiseEvent<T>(T @event) where T : Event
        {
            // MediatR中介者模式中的第二种方法，发布/订阅模式
            return _mediator.Publish(@event);
        }
    }
}
