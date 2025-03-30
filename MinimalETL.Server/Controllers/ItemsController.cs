using Microsoft.AspNetCore.Mvc;
using MinimalETL.Server.Contracts.Modules;
using MinimalETL.Server.Dtos;

namespace MinimalETL.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController: ControllerBase
    {

        private readonly ILogger<ItemsController> _logger;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IItemService _itemService;

        public ItemsController(
            ILogger<ItemsController> logger, 
            IItemService itemService,
            IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _itemService = itemService;
            _contextAccessor = contextAccessor;
        }

        [HttpGet("csv")]
        public async Task<ICollection<ItemDto>> ReadCSVFile(string path)
        {
            var contents = await _itemService.ReadCSVFile(path);
            return contents;
        }

        [HttpGet("xml")]
        public async Task<ICollection<ItemDto>> ReadXMLFile(string path)
        {
            var contents = await _itemService.ReadXMLFile(path);
            return contents;
        }

        [HttpGet("txt")]
        public async Task<ICollection<ItemDto>> ReadTXTFile(string path)
        {
            var contents = await _itemService.ReadTXTFile(path);
            return contents;
        }
    }
}
