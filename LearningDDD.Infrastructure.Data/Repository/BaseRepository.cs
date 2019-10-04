﻿using LearningDDD.Domain.IRepository;
using LearningDDD.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LearningDDD.Infrastructure.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly UserContext _dbContext;

        public BaseRepository(UserContext context)
        {
            _dbContext = context;
        }

        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>();
        }

        public TEntity GetById(Guid id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }
        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public void Remove(Guid id)
        {
            var entity = GetById(id);
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public async Task RemoveAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }
    }
}
