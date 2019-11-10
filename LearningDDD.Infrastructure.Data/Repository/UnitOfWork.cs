using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LearningDDD.Domain.IRepository;
using LearningDDD.Infrastructure.Data.Context;

namespace LearningDDD.Infrastructure.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserContext _context;

        public UnitOfWork(UserContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 上下文提交
        /// </summary>
        /// <returns></returns>
        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        /// <summary>
        /// 上下文提交
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 手动回收
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
