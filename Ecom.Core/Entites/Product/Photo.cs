using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Entites.Product
{
    public class Photo: BaseEntity<int>
    {
        public string ImagName { get; set; }
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
