using Ecom.Core.DTO;
using Ecom.Core.Entites.Product;
using Ecom.Core.Sharing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.interfaces
{
    public interface IProductRepositry:IGenericRepositry<Product>
    {
         Task<IEnumerable<ProductDto>> GetAllAsync(ProductParams productParams);
        Task<bool> AddAsyncProduct(AddProductDto productDto);
        Task<bool> UpdateAsyncProduct(UpdateProductDto updateProductDto);
        Task<bool> DeleteAsyncProduct(Product product);

    }
}
