using System.Collections.Generic;

namespace AutoMapper.QueryableExtensions.Issue2643.DTOs
{
    public class InventoryServerDto
    {
        public int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual List<InventoryServerProductLineDto> InventoryServerProductLines { get; set; }
    }
}
