using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LearningDDD.Domain.Core.Bus;
using LearningDDD.Domain.IRepository;
using Microsoft.Extensions.Caching.Memory;

namespace LearningDDD.Domain.CommandHandlers
{
    public class CommandHandler
    {
        private readonly IMediatorHandler _bus;
        private readonly IUnitOfWork _uow;
        private readonly IMemoryCache _memoryCache;

        public CommandHandler(IMediatorHandler bus
            , IUnitOfWork uow
            , IMemoryCache memoryCache)
        {
            _bus = bus;
            _uow = uow;
            _memoryCache = memoryCache;
        }

        public async Task<bool> CommitAsync()
        {
            return await _uow.CommitAsync();
        }
    }
}
