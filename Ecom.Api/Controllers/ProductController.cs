using AutoMapper;
using Ecom.Api.Helper;
using Ecom.Core.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers
{

    public class ProductController : BaseController
    {
        public ProductController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("getAll-products")]
        public async Task<IActionResult> GetAllProdut()
        {
            try
            {
                var products = await _unitOfWork.ProductRepositry
                    .GetAllAsync(x=> x.Category,x => x.Photos);
                if(products is null)
                {
                    return BadRequest(new ResponseApi(400));
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
