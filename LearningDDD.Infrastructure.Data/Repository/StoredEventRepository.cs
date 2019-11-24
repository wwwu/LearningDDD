using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningDDD.Domain.Core.Events;
using LearningDDD.Domain.IRepository;
using LearningDDD.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LearningDDD.Infrastructure.Data.Repository
{
    public class StoredEventRepository : BaseRepository<StoredEvent>, IStoredEventRepository
    {
        private readonly UserContext _dbContext;
        private readonly DbSet<StoredEvent> _dbSet;
        public StoredEventRepository(UserContext context) : base(context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<StoredEvent>();
        }

        public async Task<List<StoredEvent>> GetListByAggregateIdAsync(Guid aggregateId)
        {
            return await _dbSet.Where(s => s.AggregateId == aggregateId).ToListAsync();
        }
    }
}
