using CatalogRestApi.Data.Interfaces;
using CatalogRestApi.Models.CategoryModels;
using CatalogRestApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatalogRestApi.Services
{
    public sealed class CategoryService : ICategoryService
    {
        private readonly ICatalogDbContext _context;

        public CategoryService(ICatalogDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<CategoryDetail> CreateCategory(CategoryForCreate categoryForCreate)
        {
            if (categoryForCreate is null)
                throw new ArgumentNullException(nameof(categoryForCreate));

            return CreateCategory();

            async Task<CategoryDetail> CreateCategory()
            {
                var category = new Category
                {
                    CategoryId = new Random().Next(),
                    Title = categoryForCreate.Title
                };

                _context.Categories.Add(category);
                await _context.SaveChangesAsync().ConfigureAwait(false);

                var categoryDetail = new CategoryDetail
                {
                    CategoryId = category.CategoryId,
                    Title = categoryForCreate.Title
                };

                return categoryDetail;
            }
        }

        public async Task DeleteCategory(long categoryId)
        {
            var categoryFromDb = await _context
                .Categories
                .Where(category => category.CategoryId == categoryId)
                .Include(category => category.Items)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if (categoryFromDb == null)
                throw new EntityNotFoundException($"A rating having id '{categoryId}' could not be found");

            _context.Categories.Remove(categoryFromDb);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public Task PatchCategory(long categoryId, CategoryForPatch categoryForPatch)
        {
            if (categoryForPatch is null)
                throw new ArgumentNullException(nameof(categoryForPatch));

            return UpdateCategory();

            async Task UpdateCategory()
            {
                var categoryFromDb = await _context
                    .Categories
                    .Where(Category => Category.CategoryId == categoryId)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                if (categoryFromDb == null)
                    throw new EntityNotFoundException($"A rating having id '{categoryId}' could not be found");

                categoryFromDb.Title = categoryForPatch.Title;

                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
        }
        public async Task<IEnumerable<Category>> ListCategories()
        {
            var categories = await _context
                .Categories
                .Include(category => category.Items)
                .ToListAsync();

            if (categories == null)
                throw new EntityNotFoundException($"No categories could be found");

            return categories;
        }
    }
}
