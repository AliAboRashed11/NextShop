using AutoMapper;
using Ecom.Api.Helper;
using Ecom.Core.DTO;
using Ecom.Core.Entites.Product;
using Ecom.Core.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers
{

    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _unitOfWork.CategoryRepositry.GetAllAsync();
                if (categories is null)
                {
                    return BadRequest(new ResponseApi(400));
                }
                else
                {
                    return Ok(categories );
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepositry.GetByIdAsync(id);
                if (category is null)
                {
                    return BadRequest(new ResponseApi(400, $"not found category id ={id}"));
                }
                else
                {
                    return Ok(category);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-category")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDto categorydto)
        {
            try
            {
                var category = _mapper.Map<Category>(categorydto);
               
                await _unitOfWork.CategoryRepositry.AddAsync(category);
                return Ok(new ResponseApi(200,"Item has been Added"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-category")]
        public async Task<IActionResult> UpdateCategory( [FromBody] UpdateCategoryDto updateCategoryDto)
        {
            try
            {
                var category = _mapper.Map<Category>(updateCategoryDto);
                await _unitOfWork.CategoryRepositry.UpdateAsync(category);
                return Ok(new ResponseApi(200, "Item has been Updated"));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-category/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _unitOfWork.CategoryRepositry.DeleteAsync(id);
                return Ok(new ResponseApi(200, "Item has been Deleted"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
