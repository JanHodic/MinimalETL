using System;
namespace MinimalETL.Server.Contracts.Bases
{
    public interface IBaseReadService<Dto>
            where Dto : class
    {
        /// <summary>
        ///     Returns all records.
        /// </summary>
        /// <returns>Returns all records</returns>
        public Task<ICollection<Dto>> GetAllAsync();

        /// <summary>
        ///     Returns all records in the given interval.
        /// </summary>
        /// <param name="from">id of element where to begin.</param>
        /// <param name="to">id of element where to end.</param>
        /// <returns>Returns all objects in the given interval</returns>
        public Task<ICollection<Dto>> GetAllAsync(int page, int pageSize);
        /// <summary>
        ///     Returns record by given id.
        /// </summary>
        /// <returns>Returns the given item</returns>
        public Task<Dto> GetAsync(Guid? id);
        /// <summary>
        ///     Returns mutltiple records by given ids.
        /// </summary>
        /// <returns>Returns the given item</returns>
        public Task<ICollection<Dto>> FindAllByIds(IEnumerable<Guid> ids);
    }
}

