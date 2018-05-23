using AutoMapper.QueryableExtensions.Issue2643.DTOs;
using AutoMapper.QueryableExtensions.Issue2643.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoMapper.QueryableExtensions.Issue2643
{
    class Program
    {
        static void Main(string[] args)
        {
            Mapper.Initialize(cfg =>
                cfg.CreateMap<InventoryServer, InventoryServerDto>(MemberList.Destination));

            //MigrateAndSeed.Migrate();

            //MigrateAndSeed.Seed();

            using (var context = new MyDbContext())
            {

                // Example A: This generates a seperate query PER InventoryServer row.
                // See README.md
                //
                var records = context
                .InventoryServer
                .ProjectTo<InventoryServerDto>()
                .ToList();

                // Example B: This does not resolve the issue
                //
                //var records = context
                //.InventoryServer
                //.Include(x => x.InventoryServerProductLines)
                //    .ThenInclude(x => x.Product)
                //.ProjectTo<InventoryServerDto>()
                //.ToList();

                // Example C: This is supposed to be effectively what `ProjectTo()` does. However, this still generates extra queries.
                //
                //var records = context
                //.InventoryServer
                //.Select(x => new InventoryServerDto()
                //{
                //    Id = x.Id,
                //    Name = x.Name,
                //    InventoryServerProductLines = x.InventoryServerProductLines.Select(y => new InventoryServerProductLineDto()
                //    {
                //        InventoryServerId = y.InventoryServerId,
                //        ProductId = y.ProductId,
                //        Product = new ProductDto()
                //        {
                //            Id = y.Product.Id,
                //            Name = y.Product.Name
                //        }
                //    }).ToList()
                //})
                //.ToList();

                // Example D: No projection, no selection. Results in 2 queries.
                //
                //var records = context
                //.InventoryServer
                //.Include(x => x.InventoryServerProductLines)
                //    .ThenInclude(x => x.Product)
                //.ToList();

                Console.WriteLine($"Retrieved {records.Count} InventoryServerDtos.");

                Console.ReadLine();
            }
        }
    }
}
