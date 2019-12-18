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
    public class UserCommandHandlers : CommandHandler
        , IRequestHandler<CreateUserCommand, Unit>
        , IRequestHandler<RemoveUserCommand, Unit>
        , IRequestHandler<UpdateUserCommand, Unit>
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

            //数据持久化
            var userEntity = await _userRepository.AddReturnEntityAsync(userModel);
            if (await CommitAsync())
            {
                //领域事件
                await _bus.RaiseEvent(new UserCreatedEvent(userEntity.Id, userEntity.Name, userEntity.Email));
            }

            return result;
        }

        public async Task<Unit> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            var result = default(Unit);
            await _userRepository.RemoveAsync(request.Id);
            await CommitAsync();
            return result;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
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

            //校验
            var entity = await _userRepository.GetByIdAsync(request.Id);
            if (entity is null)
            {
                //通知
                await _bus.RaiseEvent(new DomainNotification("用户不存在！"));
                return result;
            }

            //持久化
            _mapper.Map(request, entity);
            _userRepository.Update(entity);
            await CommitAsync();

            return result;
        }
    }
}
