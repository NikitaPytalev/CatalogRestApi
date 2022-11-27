using System.Runtime.Serialization;

namespace CatalogRestApi.Models.ItemModels
{
    [DataContract(Name = "Item", Namespace = "")]
    public sealed class ItemDetail
    {
        [DataMember(Order = 1)]
        public int ItemId { get; set; }

        [DataMember(Order = 2)]
        public string Title { get; set; } = string.Empty;
    }
}