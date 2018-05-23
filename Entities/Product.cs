using System.Collections.Generic;

namespace AutoMapper.QueryableExtensions.Issue2643.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<InventoryServerProductLine> InventoryServerProductLines { get; set; }
    }
}