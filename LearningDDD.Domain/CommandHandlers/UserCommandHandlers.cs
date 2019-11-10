using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LearningDDD.Domain.Commands.User;
using LearningDDD.Domain.Core.Bus;
using LearningDDD.Domain.IRepository;
using LearningDDD.Domain.Models;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace LearningDDD.Domain.CommandHandlers
{
    public class UserCommandHandlers : CommandHandler, IRequestHandler<CreateUserCommand, Unit>
    {
        private readonly IMediatorHandler _bus;
        private readonly IUnitOfWork _uow;
        private readonly IMemoryCache _memoryCache;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserCommandHandlers(IMediatorHandler bus
            , IUnitOfWork uow
            , IMemoryCache memoryCache
            , IMapper mapper
            , IUserRepository userRepository) : base(bus, uow, memoryCache)
        {
            _bus = bus;
            _uow = uow;
            _memoryCache = memoryCache;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = default(Unit);
            //命令验证
            if (!request.IsValid())
            {
                return result;
            }

            //业务校验
            var user = _mapper.Map<User>(request);
            if (await _userRepository.AnyAsync(s => s.Email == user.Email))
            {

                return result;
            }

            //持久化
            await _userRepository.AddAsync(user);

            //提交工作单元
            if (await CommitAsync())
            {
                // 提交成功后，这里需要发布领域事件
                // 比如欢迎用户注册邮件呀，短信呀等

                // waiting....
            }

            return result;
        }
    }
}
