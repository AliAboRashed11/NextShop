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
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.HasData(
            new Photo
            {
                Id = 1,
                
                ImagName = "photo1.jpg",
             
                ProductId = 1
            },
            new Photo
            {
                Id = 2,
               
                ImagName = "photo2.jpg",
          
                ProductId = 1
            },
            new Photo
            {
                Id = 3,
                
                ImagName = "photo3.jpg",
             
                ProductId = 2
            }
        );
        }
    }
}
