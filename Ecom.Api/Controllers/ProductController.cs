using AutoMapper;
using Ecom.Api.Helper;
using Ecom.Core.DTO;
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
                    .GetAllAsync(x => x.Category, x => x.Photos);
                var result = _mapper.Map<List<ProductDto>>(products);
                if (result is null)
                {
                    return BadRequest(new ResponseApi(400));
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("getbyId/{Id}")]
        public async Task<IActionResult> GetbyId(int Id)
        {
            try
            {
                var product = await _unitOfWork.ProductRepositry
                    .GetByIdAsync(Id, x => x.Category, x => x.Photos);
                var result = _mapper.Map<ProductDto>(product);
                if (result is null)
                {
                    return BadRequest(new ResponseApi(400));
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
    }
}
