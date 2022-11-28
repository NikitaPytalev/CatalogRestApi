using CatalogRestApi.Models.ItemModels;
using System.Runtime.Serialization;

namespace CatalogRestApi.Models.CategoryModels
{
    [DataContract(Name = "Category", Namespace = "")]
    public sealed class CategoryDetail
    {
        [DataMember(Order = 1)]
        public int CategoryId { get; set; }

        [DataMember(Order = 2)]
        public string Title { get; set; } = string.Empty;

        [DataMember(Order = 3)]
        public IEnumerable<Item> Items { get; set; } = new List<Item>();
    }
}
