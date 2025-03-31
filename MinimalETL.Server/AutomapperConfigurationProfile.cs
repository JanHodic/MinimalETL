using AutoMapper;
using MinimalETL.Server.Dtos;
using MinimalETL.Server.Models;

namespace MinimalETL.Server
{
    public class AutomapperConfigurationProfile: Profile
    {
        public AutomapperConfigurationProfile()
        {
            CreateMap<ItemDto, Item>();
            CreateMap<Item, ItemDto>();
        }
    }
}
