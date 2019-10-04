using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningDDD.Domain.IRepository
{
    /// <summary>
    /// 泛型仓储接口，继承IDisposable，显式释放资源
    /// </summary>
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);/// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// 根据id获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(Guid id);
        /// <summary>
        /// 根据id获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(Guid id);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// 根据id删除
        /// </summary>
        /// <param name="id"></param>
        void Remove(Guid id);
        /// <summary>
        /// 根据id删除
        /// </summary>
        /// <param name="id"></param>
        Task RemoveAsync(Guid id);

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();
    }
}
