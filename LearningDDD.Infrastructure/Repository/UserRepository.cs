using LearningDDD.Domain.IRepository;
using LearningDDD.Domain.Models;
using LearningDDD.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningDDD.Infrastructure.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly UserContext _dbContext;
        private readonly DbSet<User> _dbSet;

        public UserRepository(UserContext context) : base(context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<User>();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(s => s.Email == email);
        }
    }
}
