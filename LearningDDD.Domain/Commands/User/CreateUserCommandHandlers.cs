using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LearningDDD.Domain.Bus;
using LearningDDD.Domain.Events.User;
using LearningDDD.Domain.IRepository;
using LearningDDD.Domain.Models.Base;
using MediatR;

namespace LearningDDD.Domain.Commands.User
{
    public class CreateUserCommandHandlers : IRequestHandler<CreateUserCommand, BaseResult>
    {
        private readonly IMediatorHandler _bus;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandlers(IMediatorHandler bus
            , IUnitOfWork uow
            , IMapper mapper
            , IUserRepository userRepository)
        {
            _bus = bus;
            _uow = uow;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<BaseResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = new BaseResult();
            //命令验证
            if (!request.IsValid())
            {
                result.Message = request.ValidationResult.Errors.FirstOrDefault()?.ErrorMessage;
                return result;
            }

            //业务校验
            var userModel = _mapper.Map<Models.User>(request);
            if (await _userRepository.AnyAsync(s => s.Email == userModel.Email))
            {
                result.Message = "该邮箱已经被使用";
                return result;
            }

            //数据持久化
            var userEntity = await _userRepository.AddReturnEntityAsync(userModel);

            //领域事件
            if (result.IsSuccess = await _uow.CommitAsync())
            {
                await _bus.RaiseEvent(new UserCreatedEvent(userEntity.Id, userEntity.Name, userEntity.Email));
            }

            return result;
        }
    }
}
