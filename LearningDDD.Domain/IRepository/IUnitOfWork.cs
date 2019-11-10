using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningDDD.Domain.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 提交工作单元
        /// </summary>
        /// <returns></returns>
        bool Commit();

        /// <summary>
        /// 提交工作单元
        /// </summary>
        /// <returns></returns>
        Task<bool> CommitAsync();
    }
}
