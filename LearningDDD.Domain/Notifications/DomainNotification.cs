using System;
using System.Collections.Generic;
using System.Text;
using LearningDDD.Domain.Events;

namespace LearningDDD.Domain.Notifications
{
    /// <summary>
    /// 领域通知模型，用来获取当前总线中出现的通知信息
    /// 继承自领域事件和 INotification
    /// </summary>
    public class DomainNotification : Event
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid DomainNotificationId { get; private set; }

        public string Key { get; private set; }

        public string Value { get; private set; }

        public int Version { get; private set; }

        public DomainNotification(string value, string key = null) : base(Guid.Empty)
        {
            DomainNotificationId = Guid.NewGuid();
            Version = 1;
            Key = key;
            Value = value;
        }
    }
}
