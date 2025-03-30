using MinimalETL.Server.Contracts.Bases;
using MinimalETL.Server.Models;

namespace MinimalETL.Server.Contracts.Modules
{
    public interface IItemRepository: IBaseReadRepository<Item>, IBaseWriteRepository<Item>
    {
    }
}
