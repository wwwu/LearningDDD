using AutoMapper;
using AutoMapper.QueryableExtensions;
using LearningDDD.Application.Interface;
using LearningDDD.Application.ViewModels.User;
using LearningDDD.Domain.Commands.User;
using LearningDDD.Domain.Bus;
using LearningDDD.Domain.IRepository;
using LearningDDD.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task AddAsync(UserVM userVM)
        {
            var command = _mapper.Map<CreateUserCommand>(userVM);
            var result = await _bus.SendCommand(command);
        }

        public IEnumerable<UserVM> GetAll()
        {
            return _userRepository.GetAll().ProjectTo<UserVM>(_mapper.ConfigurationProvider);
        }

        public async Task<UserVM> GetByIdAsync(Guid id)
        {
            var entity = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserVM>(entity);
        }

        public async Task RemoveAsync(Guid id)
        {
            await _userRepository.RemoveAsync(id);
            await _userRepository.SaveChangesAsync();
        }

        public async Task Update(UserVM userVM)
        {
            _userRepository.Update(_mapper.Map<User>(userVM));
            await _userRepository.SaveChangesAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
