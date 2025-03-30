using MinimalETL.Server.Models.Bases;

namespace MinimalETL.Server.Contracts.Bases
{
    public interface IBaseWriteRepository<T>
            where T : BaseDateEntity
    {
        /// <summary>
        ///     Creates a new record.
        /// </summary>
        /// <param name="item">DTO with new record.</param>
        /// <returns>Returns the newly created item</returns>
        public Task<T> CreateAsync(T item);
        /// <summary>
        ///     Updates a record.
        /// </summary>    
        /// <param name="id">id of searched element.</param>
        /// <param name="item">DTO with new record to uppdate.</param>
        /// <returns>Returns the updated item</returns>
        public Task<T> UpdateAsync(Guid id, T item);
        /// <summary>
        ///     Changes or creates given records records.
        /// </summary>
        /// <param name="item">Creates or updates given objects.</param>
        /// <returns>Returns the newly created records</returns>
        public Task<ICollection<Guid>> CreateOrUpdateManyAsync(ICollection<T> items);
        /// <summary>
        ///     Deletes a record.
        /// </summary>
        /// <param name="id">Record.</param>
        /// <returns>Returns if the item was deleted</returns>
        public Task<bool> DeleteAsync(Guid id);
    }
}

