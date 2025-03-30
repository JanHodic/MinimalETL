using MinimalETL.Server.Contracts.Bases;
using MinimalETL.Server.Contracts.Features;
using MinimalETL.Server.Dtos;
using MinimalETL.Server.Models;

namespace MinimalETL.Server.Contracts.Modules
{
    public interface IItemService: IBaseReadService<ItemDto>, IFileReader<ItemDto>
    {
    }
}
