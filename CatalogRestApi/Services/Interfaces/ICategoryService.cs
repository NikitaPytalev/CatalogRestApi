using CatalogRestApi.Models.CategoryModels;

namespace CatalogRestApi.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDetail> CreateCategory(CategoryForCreate categoryForCreate);
        Task DeleteCategory(long categoryId);
        Task PatchCategory(long categoryId, CategoryForPatch categoryForPatch);
        Task<IEnumerable<Category>> ListCategories();
    }
}
