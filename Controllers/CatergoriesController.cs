using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Spartan.Models;
using Spartan.Resources;
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
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryResource>> GetAllAsync()
        {
            var categories = await _categoryService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(categories);
            return resources;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveCategoryResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var category = _mapper.Map<SaveCategoryResource, Category>(resource);
            await _categoryService.SaveAsync(category);

            //if (result == null)
            //    return BadRequest("something went wrong.");

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCategoryResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var category = _mapper.Map<SaveCategoryResource, Category>(resource);
            await _categoryService.UpdateAsync(id, category);

            //if (!result.Success)
            //    return BadRequest(result.Message);

            //var categoryResource = _mapper.Map<Category, CategoryResource>(result.Category);
            //return Ok(categoryResource);

            return NotFound();
        }
    }
}
