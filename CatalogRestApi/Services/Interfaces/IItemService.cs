using CatalogRestApi.Models.ItemModels;
using System.Collection.Generic;

namespace CatalogRestApi.Services.Interfaces
{
    public interface IItemService
    {
        Task<ItemDetail> CreateItem(ItemForCreate itemForCreate);
        Task DeleteItem(long itemId);
        Task PatchItem(long itemId, ItemForPatch itemForPatch);
        Task<IEnumerable<ItemDetail>> ListItems(ItemParams itemParameters);
    }
}