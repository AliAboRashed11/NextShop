using Ecom.Core.Entites.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.DTO
{
    public record ProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public virtual List<PhotoDto> Photos { get; set; }
        public string CategoryName { get; set; }


    }

    public record PhotoDto
    {
        public string ImagName { get; set; }
        public int ProductId { get; set; }

    }
}
