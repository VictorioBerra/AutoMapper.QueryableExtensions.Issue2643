using System.Collections.Generic;

namespace AutoMapper.QueryableExtensions.Issue2643.Entities
{
    public class InventoryServer
    {
        public InventoryServer()
        {
            this.InventoryServerProductLines = new HashSet<InventoryServerProductLine>();
        }

        public int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual ICollection<InventoryServerProductLine> InventoryServerProductLines { get; set; }
    }
}
