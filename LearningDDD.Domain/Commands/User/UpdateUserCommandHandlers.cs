using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LearningDDD.Domain.IRepository;
using LearningDDD.Domain.Models.Base;
using MediatR;

namespace LearningDDD.Domain.Commands.User
{
    public class UpdateUserCommandHandlers : IRequestHandler<UpdateUserCommand, BaseResult>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandlers(IUnitOfWork uow
            , IMapper mapper
            , IUserRepository userRepository)
        {
            _uow = uow;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<BaseResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var result = new BaseResult();
            //命令验证
            if (!request.IsValid())
            {
                result.Message = request.ValidationResult.Errors.FirstOrDefault()?.ErrorMessage;
                return result;
            }

            //校验
            var entity = await _userRepository.GetByIdAsync(request.Id);
            if (entity is null)
            {
                result.Message = "用户不存在！";
                return result;
            }

            //持久化
            _mapper.Map(request, entity);
            _userRepository.Update(entity);
            result.IsSuccess = await _uow.CommitAsync();
            return result;
        }
    }
}
