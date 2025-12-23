using AutoMapper;
using Ecom.Api.Helper;
using Ecom.Core.DTO;
using Ecom.Core.Entites.Product;
using Ecom.Core.interfaces;
using Ecom.Core.Sharing;
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
        public async Task<IActionResult> GetAllProduct([FromQuery] ProductParams productParams)
        {
            try
            {
                var products = await _unitOfWork.ProductRepositry
                    .GetAllAsync(productParams);
            var totalCount = await _unitOfWork.ProductRepositry.CountAsync();
                return Ok(
                    new Pagination<ProductDto>(
                        productParams.PageNumber,
                    productParams.pageSize, 
                    totalCount,
                    products
                    ));
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

        [HttpPost("Add-Product")]
        public async Task<IActionResult> AddProduct(AddProductDto productdto)
        {
            try
            {
                await _unitOfWork.ProductRepositry.AddAsyncProduct(productdto);
                return Ok(new ResponseApi(200, "Product added successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseApi(400, ex.Message));

            }
        }


        [HttpPut("Update-Product")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            try
            {
                await _unitOfWork.ProductRepositry.UpdateAsyncProduct(updateProductDto);
                return Ok(new ResponseApi(200, "Product updated successfully"));    

            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseApi(400, ex.Message));
            }
        }

        [HttpDelete("Delete-Product/{Id}")]
        public async Task<IActionResult> DeleteProduct(int Id)
        {
            try
            {
                var product = await _unitOfWork.ProductRepositry.GetByIdAsync(Id,x=>x.Photos,x=>x.Category);
                await _unitOfWork.ProductRepositry.DeleteAsyncProduct(product);
                return Ok(new ResponseApi(200, "Product deleted successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseApi(400, ex.Message));
            }
        }

    }
}
