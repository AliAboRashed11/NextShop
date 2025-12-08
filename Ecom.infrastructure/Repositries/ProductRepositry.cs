using Ecom.Core.Entites.Product;
using Ecom.Core.interfaces;
using Ecom.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Repositries
{
    public class ProductRepositry : GenericRepostitry<Product>, IProductRepositry
    {
        public ProductRepositry(AppDContext context) : base(context)
        {
        }
    }
}
