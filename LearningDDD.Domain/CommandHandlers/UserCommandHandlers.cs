using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LearningDDD.Domain.Commands.User;
using LearningDDD.Domain.Bus;
using LearningDDD.Domain.Notifications;
using LearningDDD.Domain.Events.User;
using LearningDDD.Domain.IRepository;
using LearningDDD.Domain.Models;
using MediatR;

namespace LearningDDD.Domain.CommandHandlers
{
    public class UserCommandHandlers : CommandHandler, IRequestHandler<CreateUserCommand, Unit>
    {
        private readonly IMediatorHandler _bus;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserCommandHandlers(IMediatorHandler bus
            , IUnitOfWork uow
            , IMapper mapper
            , IUserRepository userRepository) : base(uow)
        {
            _bus = bus;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = default(Unit);
            //命令验证
            if (!request.IsValid())
            {
                //领域通知
                foreach (var error in request.ValidationResult.Errors)
                {
                    await _bus.RaiseEvent(new DomainNotification(error.ErrorMessage));
                }                
                return result;
            }

            //业务校验
            var userModel = _mapper.Map<User>(request);
            if (await _userRepository.AnyAsync(s => s.Email == userModel.Email))
            {
                //领域通知
                await _bus.RaiseEvent(new DomainNotification("该邮箱已经被使用！"));
                return result;
            }

            //持久化
            var userEntity = await _userRepository.AddReturnEntityAsync(userModel);
            //提交工作单元
            if (await CommitAsync())
            {
                //领域事件
                _ = _bus.RaiseEvent(new UserCreatedEvent(userEntity.Id, userEntity.Name, userEntity.Email));
            }

            return result;
        }
    }
}
