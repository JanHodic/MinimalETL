using AutoMapper;
using MinimalETL.Server.Commons.Bases;
using MinimalETL.Server.Contracts.Bases;
using MinimalETL.Server.Contracts.Features;
using MinimalETL.Server.Contracts.Modules;
using MinimalETL.Server.Data.Repositories;
using MinimalETL.Server.Dtos;
using MinimalETL.Server.Models;
using System.Xml;

namespace MinimalETL.Server.Services
{
    public class ItemService : BaseReadService<IItemRepository, Item, ItemDto>, IItemService
    {
        public ItemService(
            ILogger<Item> eventLogger, 
            IItemRepository repository, 
            IMapper mapper
            ) : base(eventLogger, repository, mapper)
        {
        }

        public async Task<ICollection<ItemDto>> ReadCSVFile(string path)
        {
            List<Item> items = new List<Item>();

            using (StreamReader sr = new StreamReader(path))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    string[] divided = s.Split(';');
                    string jmeno = divided[0];
                    int vek = int.Parse(divided[1]);
                    DateTime registrovan = DateTime.Parse(divided[2]);
                }
            }

            var createdIds = await _repository.CreateOrUpdateManyAsync(items);

            var itemDtos = await this.FindAllByIds(createdIds);
            return itemDtos;
        }

        public async Task<ICollection<ItemDto>> ReadTXTFile(string path)
        {
            List<Item> items = new List<Item>();

            using (StreamReader sr = new StreamReader(@"soubor.txt"))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }

            var createdIds = await _repository.CreateOrUpdateManyAsync(items);

            var itemDtos = await this.FindAllByIds(createdIds);
            return itemDtos;
        }

        public async Task<ICollection<ItemDto>> ReadXMLFile(string path)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(path);
            XmlNode root = xmlDocument.DocumentElement;
            List<Item> items = new List<Item>();
            foreach (XmlNode node in root.ChildNodes)
            {
                int id = 0;

                if (node.Name=="id")
                {
                    XmlElement element = (XmlElement)node;
                    id = int.Parse(element.GetAttribute("id"));
                }
                if(node.Name=="items")
                {
                    foreach(XmlNode item in node.ChildNodes)
                    {
                        if (item.Name=="item")
                        {
                            Item i = new Item() { DocumentId = id };
                            XmlElement subElement = (XmlElement)item;
                            i.ProductName = subElement.GetAttribute("itemName");

                            XmlElement quantityNodes = (XmlElement)subElement.GetElementsByTagName("itemQty")[0];
                            i.Quantity = int.Parse(quantityNodes.GetAttribute("quantity"));
                            i.Unit = quantityNodes.GetAttribute("unit");
                            i.UnitPrice = int.Parse(subElement.GetAttribute("costPerUnitNet"));
                            i.VATPercentage = int.Parse(subElement.GetAttribute("vatRate"));
                            items.Add(i);
                        }
                    }
                }
            }

            



            var createdIds = await _repository.CreateOrUpdateManyAsync(items);
            
            var itemDtos = await this.FindAllByIds(createdIds);
            return itemDtos;
        }
    }
}
