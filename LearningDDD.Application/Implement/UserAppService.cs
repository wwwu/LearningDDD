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

namespace LearningDDD.Application.Implement
{.

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

        public async Task AddAsync(CreateUserDto dto)
        {
            var command = _mapper.Map<CreateUserCommand>(dto);
            _ = await _bus.SendCommand(command);
        }

        public async Task RemoveAsync(Guid id)
        {
            _ = await _bus.SendCommand(new RemoveUserCommand(id));
        }

        public async Task Update(UpdateUserDto dto)
        {
            var command = _mapper.Map<UpdateUserCommand>(dto);
            _ = await _bus.SendCommand(command);
        }

        public IEnumerable<UserDto> GetAll()
        {
            return _userRepository.GetAll().ProjectTo<UserDto>(_mapper.ConfigurationProvider);
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
