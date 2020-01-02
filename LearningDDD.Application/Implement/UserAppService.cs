using AutoMapper;
using AutoMapper.QueryableExtensions;
using LearningDDD.Application.Interface;
using LearningDDD.Application.Dto.User;
using LearningDDD.Domain.Commands.User;
using LearningDDD.Domain.Bus;
using LearningDDD.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using LearningDDD.Domain.Models.Base;

namespace LearningDDD.Application.Implement
{

    public class UserAppService : IUserAppService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IMediatorHandler _bus;

        public UserAppService(IMapper mapper
            , IUserRepository userRepository
            , IMediatorHandler bus)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _bus = bus;
        }

        public async Task<BaseResult> AddAsync(CreateUserDto dto)
        {
            var command = _mapper.Map<CreateUserCommand>(dto);
            var result = await _bus.SendCommand<CreateUserCommand, BaseResult>(command);
            return result;
        }

        public async Task<bool> RemoveAsync(Guid id)
        {
            var result = await _bus.SendCommand<RemoveUserCommand,bool>(new RemoveUserCommand(id));
            return result;
        }

        public async Task<BaseResult> Update(UpdateUserDto dto)
        {
            var command = _mapper.Map<UpdateUserCommand>(dto);
            var result = await _bus.SendCommand<UpdateUserCommand, BaseResult>(command);
            return result;
        }

        public IEnumerable<UserDto> GetAll()
        {
            return _userRepository.GetAll(tracking: false)
                .OrderByDescending(s => s.Id)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .AsEnumerable();
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var entity = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserDto>(entity);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
