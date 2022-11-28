using CatalogRestApi.Data.Interfaces;
using CatalogRestApi.Models.CategoryModels;
using CatalogRestApi.Models.ItemModels;
using CatalogRestApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogRestApi.Services
{
    public sealed class ItemService : IItemService
    {
        private readonly ICatalogDbContext _context;

        public ItemService(ICatalogDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<ItemDetail> CreateItem(ItemForCreate itemForCreate)
        {
            if (itemForCreate is null)
                throw new ArgumentNullException(nameof(itemForCreate));

            return CreateItem();

            async Task<ItemDetail> CreateItem()
            {
                var category = _context
                    .Categories
                    .Where(category => category.Title == itemForCreate.CategoryTitle)
                    .FirstOrDefault();

                var itemId = new Random().Next();

                if (category == null)
                {
                    var item = new Item
                    {
                        ItemId = itemId,
                        Title = itemForCreate.Title,
                    };

                    var newCategory = new Category
                    {
                        CategoryId = new Random().Next(),
                        Title = itemForCreate.CategoryTitle
                    };

                    newCategory.Items.Add(item);

                    _context.Categories.Add(newCategory);
                } else
                {
                    var item = new Item
                    {
                        ItemId = itemId,
                        Title = itemForCreate.Title,
                        Category = category
                    };

                    _context.Items.Add(item);
                }

                await _context.SaveChangesAsync().ConfigureAwait(false);

                var itemDetail = new ItemDetail {
                    ItemId = itemId,
                    Title = itemForCreate.Title,
                    CategoryTitle = itemForCreate.CategoryTitle
                };

                return itemDetail;
            }
        }

        public async Task DeleteItem(long itemId)
        {
            var itemFromDb = await _context
                .Items
                .Where(item => item.ItemId == itemId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if (itemFromDb == null)
                throw new EntityNotFoundException($"An item having id '{itemId}' could not be found");

            _context.Items.Remove(itemFromDb);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public Task PatchItem(long itemId, ItemForPatch itemForPatch)
        {
            if (itemForPatch is null)
                throw new ArgumentNullException(nameof(itemForPatch));

            return UpdateItem();

            async Task UpdateItem()
            {
                var itemFromDb = await _context
                    .Items
                    .Where(item => item.ItemId == itemId)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                if (itemFromDb == null)
                    throw new EntityNotFoundException($"An item having id '{itemId}' could not be found");

                itemFromDb.Title = itemForPatch.Title;

                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<ItemDetail>> ListItems(ItemParams itemParams)
        {
            if (itemParams is null)
                throw new ArgumentNullException(nameof(itemParams));

            var items = await _context.Items
                .Include(item => item.Category)
                .Where(item => item.Category.CategoryId == itemParams.CategoryId)
                .Skip((itemParams.PageNumber - 1) * itemParams.PageSize)
                .Take(itemParams.PageSize)
                .ToListAsync();

            if (items == null)
                throw new EntityNotFoundException($"No items could be found");

            var itemsDetail = items.Select(item => new ItemDetail
            {
                ItemId = item.ItemId,
                Title = item.Title,
                CategoryTitle = item.Category.Title
            });

            return itemsDetail;
        }

    }
}
