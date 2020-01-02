using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LearningDDD.Domain.IRepository;
using MediatR;

namespace LearningDDD.Domain.Events.User
{
    public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserRepository _userRepositor;

        public UserCreatedEventHandler(IUnitOfWork uow
            , IUserRepository userRepositor)
        {
            _uow = uow;
            _userRepositor = userRepositor;
        }

        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            //给名字加个符号
            var entity = await _userRepositor.GetByIdAsync(notification.Id);
            entity.Name = "_" + entity.Name;
            _userRepositor.Update(entity);
            await _uow.CommitAsync();
        }
    }
}
