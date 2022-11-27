using CatalogRestApi.Models.CategoryModels;
using CatalogRestApi.Models.ItemModels;
using Microsoft.EntityFrameworkCore;

namespace CatalogRestApi.Data.Interfaces
{
    public interface ICatalogDbContext
    {
        DbSet<Category> Categories { get; }
        DbSet<Item> Items { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
