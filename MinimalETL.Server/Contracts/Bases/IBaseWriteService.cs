using System;
namespace MinimalETL.Server.Contracts.Bases
{
    /// <summary>
    ///     Operations for changing records from database.
    /// </summary>
    public interface IBaseWriteService<Dto, DtoCreate>
        where Dto : class
        where DtoCreate : class
    {
        /// <summary>
        ///     Creates a new record.
        /// </summary>
        /// <param name="item">DTO with new record.</param>
        /// <returns>Returns the newly created item</returns>
        public Task<Dto> CreateAsync(DtoCreate item);
        /// <summary>
        ///     Updates a record.
        /// </summary>    
        /// <param name="id">id of searched element.</param>
        /// <param name="item">DTO with new record to uppdate.</param>
        /// <returns>Returns the updated item</returns>
        public Task<Dto> UpdateAsync(Guid id, Dto item);
        /// <summary>
        ///     Changes or creates given records records.
        /// </summary>
        /// <param name="item">Creates or updates given objects.</param>
        /// <returns>Returns the newly created items</returns>
        public Task<ICollection<Dto>> CreateOrUpdateManyAsync(ICollection<Dto> items);
        /// <summary>
        ///     Deletes a record.
        /// </summary>
        /// <param name="id">Item id.</param>
        /// <returns>Returns if the item was deleted</returns>
        public Task<bool> DeleteAsync(Guid id);
    }
}

