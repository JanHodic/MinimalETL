using MinimalETL.Commons.Bases;
using MinimalETL.Server.Contracts.Modules;
using MinimalETL.Server.Data.Contexts;
using MinimalETL.Server.Models;

namespace MinimalETL.Server.Data.Repositories
{
    public class ItemRepository : BaseWriteRepository<Item, TestETLDbContext>, IItemRepository
    {
        public ItemRepository(ILogger<Item> eventLogger, TestETLDbContext dbEntity) : base(eventLogger, dbEntity)
        {
        }
    }
}
