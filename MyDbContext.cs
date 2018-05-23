using AutoMapper.QueryableExtensions.Issue2643.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AutoMapper.QueryableExtensions.Issue2643
{
    public class MyDbContext : DbContext
    {
        public virtual DbSet<InventoryServer> InventoryServer { get; set; }
        public virtual DbSet<InventoryServerProductLine> InventoryProductLine { get; set; }
        public virtual DbSet<Product> Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var migrationsAssembly = typeof(MyDbContext).GetTypeInfo().Assembly.GetName().Name;

            optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;database=Issue2643;trusted_connection=yes", sql =>
            {
                sql.MigrationsAssembly(migrationsAssembly);
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define composite key
            modelBuilder.Entity<InventoryServerProductLine>()
                .HasKey(bc => new { bc.InventoryServerId, bc.ProductId });

            // This explicit configuration is not needed, as I am properly naming my keys as per convention
            // https://www.learnentityframeworkcore.com/conventions#foreign-key
            // Testing has determined it does not change the query output
            //modelBuilder.Entity<InventoryServerProductLine>()
            //    .HasOne(bc => bc.Product)
            //    .WithMany(b => b.InventoryServerProductLines)
            //    .HasForeignKey(bc => bc.ProductId);

            //modelBuilder.Entity<InventoryServerProductLine>()
            //    .HasOne(bc => bc.InventoryServer)
            //    .WithMany(c => c.InventoryServerProductLines)
            //    .HasForeignKey(bc => bc.InventoryServerId);
        }

    }
}
