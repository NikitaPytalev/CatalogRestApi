using CatalogRestApi.Data.Interfaces;
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
                var item = new Item
                {
                    ItemId =new Random().Next(),
                    Title = itemForCreate.Title
                };
                
                _context.Items.Add(item);
                await _context.SaveChangesAsync().ConfigureAwait(false);

                var itemDetail = new ItemDetail {
                    ItemId = item.ItemId,
                    Title = itemForCreate.Title
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
                throw new EntityNotFoundException($"A rating having id '{itemId}' could not be found");

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
                    throw new EntityNotFoundException($"A rating having id '{itemId}' could not be found");

                itemFromDb.Title = itemForPatch.Title;

                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
        }
    }
}
