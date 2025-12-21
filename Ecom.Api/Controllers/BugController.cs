using AutoMapper;
using Ecom.Core.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers
{
 
    public class BugController : BaseController
    {
        public BugController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("not-found")]
        public async Task<IActionResult> GetNotFound()
        {
            var category = await _unitOfWork.CategoryRepositry.GetByIdAsync(100);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpGet("server-error")]
        public async Task<IActionResult> GetServerError()
        {
            var category = await _unitOfWork.CategoryRepositry.GetByIdAsync(0);
            category.Name = ""; // This will cause a server error if category is null
            return Ok(category);
        }

        [HttpGet("bad-request/{Id}")]
        public async Task<IActionResult> GetBadRequest(int Id)
        {
            return Ok();  
        }

        [HttpGet("bad-request")]
        public async Task<IActionResult> GetBadRequest()
        {
            return BadRequest();
        }
    }
}
