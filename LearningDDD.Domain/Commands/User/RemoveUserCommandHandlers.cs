using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LearningDDD.Domain.IRepository;
using MediatR;

namespace LearningDDD.Domain.Commands.User
{
    public class RemoveUserCommandHandlers : IRequestHandler<RemoveUserCommand, bool>
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserRepository _userRepository;

        public RemoveUserCommandHandlers(IUnitOfWork uow
            , IUserRepository userRepository)
        {
            _uow = uow;
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            await _userRepository.RemoveAsync(request.Id);
            return await _uow.CommitAsync();
        }
    }
}
