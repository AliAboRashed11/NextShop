using Ecom.Core.Entites.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name)
                   .IsRequired();
           builder.Property(p => p.Description)
                   .IsRequired();
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.HasData(
                new Product { Id = 1, Name = "Smartphone", Description = "Latest model smartphone with advanced features", Price = 699.99m, CategoryId = 1 },
                new Product { Id = 2, Name = "Laptop", Description = "High-performance laptop for work and gaming", Price = 1299.99m, CategoryId = 1 },
                new Product { Id = 3, Name = "Novel", Description = "Bestselling fiction novel by a renowned author", Price = 19.99m, CategoryId = 2 },
                new Product { Id = 4, Name = "T-Shirt", Description = "Comfortable cotton t-shirt in various sizes", Price = 14.99m, CategoryId = 3 },
                new Product { Id = 5, Name = "Blender", Description = "Powerful kitchen blender for smoothies and more", Price = 89.99m, CategoryId = 4 }
            );



        }
    }
}
