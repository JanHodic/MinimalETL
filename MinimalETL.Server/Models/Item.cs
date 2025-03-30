using MinimalETL.Server.Models.Bases;

namespace MinimalETL.Server.Models
{
    public class Item: BaseDateEntity
    {
        /// <summary>
        /// Číslo dokumentu
        /// </summary>
        public int DocumentId { get; set; }
        /// <summary>
        /// Název produktu
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// Katalogový kód produktu
        /// </summary>
        public string CatalogCode { get; set; } = new Guid().ToString();
        /// <summary>
        /// Množství
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Jednotkovou cenu 
        /// </summary>
        public double UnitPrice { get; set; }
        /// <summary>
        /// Jednotka
        /// </summary>
        public string Unit { get; set; } = "j";
        /// <summary>
        /// Sazba DPH
        /// </summary>
        public double VATPercentage { get; set; }
    }
}
