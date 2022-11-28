using Microsoft.AspNetCore.Mvc;
using CatalogRestApi.Services.Interfaces;
using CatalogRestApi.Models.ItemModels;
using CatalogRestApi.Models.CategoryModels;
using CatalogRestApi.Services;

namespace CatalogRestApi.Controllers
{
    [Route("items")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService ?? throw new ArgumentNullException(nameof(itemService));
        }

        /// <summary>
        /// Create a new item
        /// </summary>
        /// <response code="201">The item was created successfully.</response>
        [HttpPost(Name = nameof(CreateItem))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateItem([FromBody] ItemForCreate item)
        {
            var createdItem = await _itemService.CreateItem(item).ConfigureAwait(true);

            return Created($"items/{createdItem.ItemId}", createdItem);
        }

        /// <summary>
        /// Delete item
        /// </summary>
        /// <response code="204">The item was deleted successfully.</response>
        [HttpDelete("{itemId:long}", Name = nameof(DeleteItem))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteItem([FromRoute] long itemId)
        {
            await _itemService.DeleteItem(itemId).ConfigureAwait(true);
            return NoContent();
        }

        /// <summary>
        /// Do partial item update
        /// </summary>
        /// <response code="204">The item was updated successfully</response>
        [HttpPatch("{itemId:long}", Name = nameof(PatchItem))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PatchItem([FromRoute] long itemId, [FromBody] ItemForPatch patch)
        {
            await _itemService.PatchItem(itemId, patch).ConfigureAwait(true);

            return NoContent();
        }


        /// <summary>
        /// Retrieve items
        /// </summary>
        /// <response code="200">The items were retrieved successfully</response>
        [HttpGet(Name = nameof(ListItems))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ItemDetail>>> ListItems([FromQuery] ItemParams itemParams)
        {
            var items = await _itemService.ListItems(itemParams).ConfigureAwait(true);

            return Ok(items);
        }
    }
}