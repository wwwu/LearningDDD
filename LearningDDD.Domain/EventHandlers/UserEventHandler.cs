using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LearningDDD.Domain.Events.User;
using LearningDDD.Domain.IRepository;
using MediatR;

namespace LearningDDD.Domain.EventHandlers
{
    public class UserEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserRepository _userRepositor;

        public UserEventHandler(IUnitOfWork uow
            , IUserRepository userRepositor)
        {
            _uow = uow;
            _userRepositor = userRepositor;
        }

        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            //1,发送邮件
            Thread.Sleep(2000);
            //2,给每个名字加个符号
            var entity = await _userRepositor.GetByIdAsync(notification.Id);
            entity.Name = "_" + entity.Name;
            _userRepositor.Update(entity);
            await _uow.CommitAsync();
        }
    }
}
