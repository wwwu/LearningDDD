using AutoMapper;
using AutoMapper.QueryableExtensions;
using LearningDDD.Application.Interface;
using LearningDDD.Application.ViewModels;
using LearningDDD.Domain.IRepository;
using LearningDDD.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearningDDD.Application.Implement
{
    public class UserAppService : IUserAppService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserAppService(IMapper mapper
            , IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task AddAsync(UserVM userVM)
        {
            await _userRepository.AddAsync(_mapper.Map<User>(userVM));
            await _userRepository.SaveChangesAsync();
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
