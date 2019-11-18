using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LearningDDD.Domain.Events.User;
using MediatR;

namespace LearningDDD.Domain.EventHandlers
{
    public class UserEventHandler : INotificationHandler<UserCreatedEvent>
    {
        public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            //1,发送邮件
            Thread.Sleep(2000);
            //2,站内信


            return Task.CompletedTask;
        }
    }
}
