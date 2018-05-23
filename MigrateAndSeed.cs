using AutoMapper.QueryableExtensions.Issue2643.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoMapper.QueryableExtensions.Issue2643
{
    public static class MigrateAndSeed
    {
        public static void Migrate()
        {
            using (var context = new MyDbContext())
            {
                Console.WriteLine($"Migrating.");

                context.Database.Migrate();
            }
        }

        public static void Seed()
        {
            using (var context = new MyDbContext())
            {
                Console.WriteLine($"Seeding.");

                if (!context.InventoryServer.Any())
                {
                    // Build out the graph
                    context.InventoryServer.AddRange(new List<InventoryServer>() {
                        new InventoryServer()
                        {
                            Name = "Test Inventory Server 1",
                            InventoryServerProductLines = new List<InventoryServerProductLine>()
                            {
                                new InventoryServerProductLine()
                                {
                                    Product = new Product()
                                    {
                                        Name = "Test Product 1"
                                    }
                                },
                                new InventoryServerProductLine()
                                {
                                    Product = new Product()
                                    {
                                        Name = "Test Product 2"
                                    }
                                }
                            }
                        },
                        new InventoryServer()
                        {
                            Name = "Test Inventory Server 2",
                            InventoryServerProductLines = new List<InventoryServerProductLine>()
                            {
                                new InventoryServerProductLine()
                                {
                                    Product = new Product()
                                    {
                                        Name = "Test Product 3"
                                    }
                                },
                                new InventoryServerProductLine()
                                {
                                    Product = new Product()
                                    {
                                        Name = "Test Product 4"
                                    }
                                }
                            }
                        }
                    });

                    context.SaveChanges();
                }
            }

        }


    }
}
