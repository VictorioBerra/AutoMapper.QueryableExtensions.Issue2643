namespace AutoMapper.QueryableExtensions.Issue2643.Entities
{
    public class InventoryServerProductLine
    {

        public virtual int InventoryServerId { get; set; }

        public virtual int ProductId { get; set; }

        public InventoryServer InventoryServer { get; set; }

        public Product Product { get; set; }
    }
}
