using LearningDDD.Domain.IRepository;
using LearningDDD.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningDDD.Infrastructure.Data.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public User GetByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
