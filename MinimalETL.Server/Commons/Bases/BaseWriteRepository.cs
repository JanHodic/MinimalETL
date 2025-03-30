using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MinimalETL.Server.Commons.Bases;
using MinimalETL.Server.Contracts.Bases;
using MinimalETL.Server.Models.Bases;

namespace MinimalETL.Commons.Bases
{
    public class BaseWriteRepository<T, TDbContext> : BaseReadRepository<T, TDbContext>,
                IBaseWriteRepository<T>, IBaseReadRepository<T>
                where T : BaseDateEntity
                where TDbContext : DbContext
    {
        /// <summary>
        ///     Writing features for database.
        /// </summary>
        /// <param name="eventLogger">Event logger variable.</param>
        /// <param name="dbEntity">Database entity.</param>
        public BaseWriteRepository(ILogger<T> eventLogger, TDbContext dbEntity) : base(eventLogger, dbEntity)
        {

        }

        public virtual async Task<T> CreateAsync(T item)
        {
            EntityEntry<T> entityEntry = _dbSet.Add(item);
            try
            {
                await _dbEntity.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                new Exception("new content not saved");
            }

            new Exception("new entity added");
            return entityEntry.Entity;
        }

        public virtual async Task<ICollection<Guid>> CreateOrUpdateManyAsync(ICollection<T> items)
        {
            var os = new List<Guid>();
            foreach (var item in items)
            {
                var e = await this.CreateAsync(item);
                os.Add(e.Id);
            }
            return os;
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            T? entity = await _dbSet.FindAsync(id);

            if (entity is null)
            {
                _eventLogger.LogWarning("Entity not found");
                return false;
            }
            try
            {
                _dbSet.Remove(entity);
                await _dbEntity.SaveChangesAsync();
                _eventLogger.LogWarning("Entity deleted");
                return true;
            }
            catch
            {
                _dbEntity.Entry(entity).State = EntityState.Unchanged;
                _eventLogger.LogWarning("Entity found but not deleted");
                throw;
            }
        }

        public virtual async Task<T> UpdateAsync(Guid id, T item)
        {
            try
            {
                EntityEntry<T> entityEntry = _dbSet.Update(item);
                await _dbEntity.SaveChangesAsync();
                _eventLogger.LogInformation("Entity created");
                return entityEntry.Entity;
            }
            catch
            {
                if (this.ExistsWithId(item.Id).Result)
                {
                    _eventLogger.LogWarning("Entity found but not updated");
                }
                else
                {
                    _eventLogger.LogError("Entity not found");
                }
                throw;
            }
        }

        protected async Task<bool> ExistsWithId(Guid id)
        {
            T? entity = await _dbSet.FindAsync(id);
            if (entity == null) return false;
            {
                _eventLogger.LogError("Entity not found");
            }
            // Abychom se vyhnuli problémům se sledováním více entit se stejným ID
            if (entity is not null)
            {
                _dbEntity.Entry(entity).State = EntityState.Detached;
            }
            _eventLogger.LogInformation("Entity found");
            return entity is not null;
        }
    }
}

