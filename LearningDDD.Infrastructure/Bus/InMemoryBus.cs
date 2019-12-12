﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LearningDDD.Domain.Bus;
using LearningDDD.Domain.Commands;
using LearningDDD.Domain.Events;
using LearningDDD.Infrastructure.EventSourcing;
using MediatR;

namespace LearningDDD.Infrastructure.Bus
{
    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventStoreService _eventStoreService;

        public InMemoryBus(IMediator mediator,
            IEventStoreService eventStoreService)
        {
            _mediator = mediator;
            _eventStoreService = eventStoreService;
        }

        public Task<Unit> SendCommand<T>(T command) where T : Command
        {
            //存储命令，由于存储在同一个数据库(上下文)，此处必须Wait执行完成
            _eventStoreService.Save(command).Wait();
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
            //除了领域通知以外的事件都保存下来
            if ((@event is Domain.Notifications.DomainNotification) == false)
                _eventStoreService.Save(@event).Wait();

            //MediatR中介者模式中的第二种方法，发布/订阅模式
            return _mediator.Publish(@event);
        }
    }
}
