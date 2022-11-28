using CatalogRestApi.Models.CategoryModels;
using CatalogRestApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogRestApi.Controllers
{
    [Route("categories")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        }

        /// <summary>
        /// Create a new category
        /// </summary>
        /// <response code="201">The category was created successfully.</response>
        [HttpPost(Name = nameof(CreateCategory))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryForCreate category)
        {
            var createdcategory = await _categoryService.CreateCategory(category).ConfigureAwait(true);

            return Created($"categories/{createdcategory.CategoryId}", createdcategory);
        }

        /// <summary>
        /// Delete category
        /// </summary>
        /// <response code="204">The category was deleted successfully.</response>
        [HttpDelete("{categoryId:long}", Name = nameof(DeleteCategory))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCategory([FromRoute] long categoryId)
        {
            await _categoryService.DeleteCategory(categoryId).ConfigureAwait(true);
            return NoContent();
        }

        /// <summary>
        /// Do partial category update
        /// </summary>
        /// <response code="204">The category was updated successfully</response>
        [HttpPatch("{categoryId:long}", Name = nameof(PatchCategory))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PatchCategory([FromRoute] long categoryId, [FromBody] CategoryForPatch patch)
        {
            await _categoryService.PatchCategory(categoryId, patch).ConfigureAwait(true);

            return NoContent();
        }

        /// <summary>
        /// Retrieve categories
        /// </summary>
        /// <response code="200">The categories were retrieved successfully</response>
        [HttpGet(Name = nameof(ListCategories))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Category>>> ListCategories()
        {
            var categories = await _categoryService.ListCategories().ConfigureAwait(true);

            return Ok(categories);
        }
    }
}
