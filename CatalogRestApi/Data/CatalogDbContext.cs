using CatalogRestApi.Data.Interfaces;
using CatalogRestApi.Models.CategoryModels;
using CatalogRestApi.Models.ItemModels;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CatalogRestApi.Data
{
    public sealed class CatalogDbContext : DbContext, ICatalogDbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Item> Items { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
                throw new ArgumentNullException(nameof(modelBuilder));

            //modelBuilder.Entity<Category>()
            //    .HasMany(c => c.Items)
            //    .WithOne(i => i.Category)
            //    .HasForeignKey(i => i.CategoryId);

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
        }
    }
}
