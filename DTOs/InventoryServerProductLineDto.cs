namespace AutoMapper.QueryableExtensions.Issue2643.DTOs
{
    public class InventoryServerProductLineDto
    {
        public virtual int InventoryServerId { get; set; }

        public virtual int ProductId { get; set; }

        public ProductDto Product { get; set; }
    }
}
