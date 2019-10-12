using Microsoft.AspNetCore.Mvc;
using Spartan.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Spartan.Extensions;
using Spartan.Interfaces;
using Microsoft.Extensions.Logging;
using Spartan.Common;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Spartan.Controllers
{
    [Authorize]
    [Route("/api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger)
        {
            _logger = logger;
            _categoryService = categoryService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var categories = await _categoryService.ListAsync();
            return categories;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Category category)
        {
            _logger?.LogDebug("'{0}' has been invoked", nameof(PostAsync));

            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            IResponse response = new Response();
            try
            {
                await _categoryService.SaveAsync(category);
            }
            catch (Exception ex)
            {
                response.HasError = true;
                response.ErrorMessage = "There was an internal error, please contact to technical support.";

                _logger?.LogCritical("There was an error on '{0}' invocation: {1}", nameof(PostAsync), ex);
            }
            return response.ToHttpResponse();
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
