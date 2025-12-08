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
    public class CategoryRepositry : GenericRepostitry<Category>, ICategoryRepositry
    {
        public CategoryRepositry(AppDContext context) : base(context)
        {
        }
    }
}
