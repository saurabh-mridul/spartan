using Microsoft.AspNetCore.Mvc;
using Spartan.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Spartan.Extensions;
using Spartan.Interfaces;

namespace Spartan.Controllers
{
    [Route("/api/[controller]")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var categories = await _categoryService.ListAsync();
            return categories;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            await _categoryService.SaveAsync(category);

            //if (result == null)
            //    return BadRequest("something went wrong.");

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            await _categoryService.UpdateAsync(id, category);

            //if (!result.Success)
            //    return BadRequest(result.Message);

            //var categoryResource = _mapper.Map<Category, CategoryResource>(result.Category);
            //return Ok(categoryResource);

            return NotFound();
        }
    }
}
