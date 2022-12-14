using CatalogRestApi.Models.ItemModels;

namespace CatalogRestApi.Models.CategoryModels
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<Item> Items { get; set; } = new List<Item>();
    }
}
