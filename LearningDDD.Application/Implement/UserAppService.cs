using LearningDDD.Application.Interface;
using LearningDDD.Application.ViewModels;
using LearningDDD.Domain.IRepository;
using System;
using System.Collections.Generic;

namespace LearningDDD.Application.Implement
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _userRepository;

        public UserAppService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Add(UserVM userVM)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }

        public IEnumerable<UserVM> GetAll()
        {
            throw new NotImplementedException();
        }

        public UserVM GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(UserVM userVM)
        {
            throw new NotImplementedException();
        }
    }
}
