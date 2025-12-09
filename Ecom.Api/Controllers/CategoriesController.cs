using Ecom.Core.DTO;
using Ecom.Core.Entites.Product;
using Ecom.Core.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers
{

    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork unitOfWork) : base(unitOfWork)
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
                    return BadRequest();
                }
                else
                {
                    return Ok(categories);
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
                    return BadRequest();
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
                var category = new Category()
                {
                    Name = categorydto.Name,
                    Description = categorydto.Description
                };
                await _unitOfWork.CategoryRepositry.AddAsync(category);
                return Ok(new {massage ="Item has been Addedd"});
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
             var category =  new Category()
                {
                    Id = updateCategoryDto.Id,
                    Name = updateCategoryDto.Name,
                    Description = updateCategoryDto.Description
                };
                await _unitOfWork.CategoryRepositry.UpdateAsync(category);
                return Ok(new { massage = "Item has been Updated" });

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
                return Ok(new { massage = "Item has been Deleted" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
