using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LearningDDD.Domain.Core.Events;

namespace LearningDDD.Domain.IRepository
{
    public interface IStoredEventRepository: IBaseRepository<StoredEvent>
    {
        Task<List<StoredEvent>> GetListByAggregateIdAsync(Guid aggregateId);
    }
}
