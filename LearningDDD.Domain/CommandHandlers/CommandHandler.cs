using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LearningDDD.Domain.IRepository;

namespace LearningDDD.Domain.CommandHandlers
{
    public class CommandHandler
    {
        private readonly IUnitOfWork _uow;

        public CommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<bool> CommitAsync()
        {
            return await _uow.CommitAsync();
        }
    }
}
