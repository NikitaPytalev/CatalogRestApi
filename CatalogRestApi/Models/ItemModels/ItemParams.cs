using Microsoft.AspNetCore.Mvc;

namespace CatalogRestApi.Models.ItemModels
{
    public class ItemParams
    {
        const int maxPageSize = 10;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 2;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        public int CategoryId { get; set; } = 0;
    }
}
