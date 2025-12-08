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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(30);
            builder.Property(c => c.Id).IsRequired();
            builder.HasData(
          new Category { Id = 1, Name = "Electronics", Description = "All electronic items" },
          new Category { Id = 2, Name = "Books", Description = "All kinds of books" },
          new Category { Id = 3, Name = "Clothing", Description = "Men, Women, and Kids clothing" },
          new Category { Id = 4, Name = "Home & Kitchen", Description = "Home and kitchen products" },
          new Category { Id = 5, Name = "Sports & Outdoors", Description = "Sports and outdoor equipment" }
      );
        }
    }
}
