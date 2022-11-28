using CatalogRestApi.Models.CategoryModels;

namespace CatalogRestApi.Models.ItemModels
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Title { get; set; } = string.Empty;
        public Category? Category { get; set; }
    }
}
