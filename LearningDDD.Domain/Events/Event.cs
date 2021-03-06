﻿using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace LearningDDD.Domain.Events
{
    /// <summary>
    /// 事件模型 抽象基类，继承 INotification
    /// </summary>
    public abstract class Event : EventBase, INotification
    {
        protected Event(Guid aggregateId) : base(aggregateId)
        {
        }
    }
}
