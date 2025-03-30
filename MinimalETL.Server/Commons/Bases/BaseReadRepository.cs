using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MinimalETL.Server.Contracts.Bases;
using MinimalETL.Server.Models.Bases;

namespace MinimalETL.Server.Commons.Bases
{
    public class BaseReadRepository<T, TDbContext> : IBaseReadRepository<T>
        where T : BaseDateEntity
        where TDbContext : DbContext
    {
        protected ILogger<T> _eventLogger;
        protected readonly TDbContext _dbEntity;
        protected readonly DbSet<T> _dbSet;

        /// <summary>
        ///     Reading features for database.
        /// </summary>
        /// <param name="eventLogger">Event logger variable.</param>
        /// <param name="dbEntity">Database entity.</param>
        public BaseReadRepository(ILogger<T> eventLogger, TDbContext dbEntity)
        {
            _dbEntity = dbEntity;
            _dbSet = dbEntity.Set<T>();
            _eventLogger = eventLogger;
        }

        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync<T>();
        }

        public virtual async Task<ICollection<T>> GetAllAsync(int page, int pageSize)
        {
            return await _dbSet
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public virtual async Task<T> GetAsync(Guid? id)
        {
            var result = await _dbSet.FindAsync(id);
            if (result == null) throw new Exception("Not found");
            else return result;
        }

        public virtual async Task<ICollection<T>> FindAllByIds(IEnumerable<Guid> ids)
        {
            var items = new List<T>();
            foreach (var id in ids)
            {
                var item = await _dbSet.FindAsync(id);
                items.Add(item);
            }
            return items;
        }
    }
}