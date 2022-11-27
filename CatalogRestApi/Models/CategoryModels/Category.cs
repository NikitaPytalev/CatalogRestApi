using CatalogRestApi.Models.ItemModels;

namespace CatalogRestApi.Models.CategoryModels
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Title { get; set; } = string.Empty;
        public virtual ICollection<Item> Items { get; } = new List<Item>();
    }
}
