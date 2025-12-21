using Ecom.Core.DTO;
using Ecom.Core.Entites.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.interfaces
{
    public interface IProductRepositry:IGenericRepositry<Product>
    {
        Task<bool> AddAsyncProduct(AddProductDto productDto);
        Task<bool> UpdateAsyncProduct(UpdateProductDto updateProductDto);
        Task<bool> DeleteAsyncProduct(Product product);
    }
}
