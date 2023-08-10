using Microsoft.EntityFrameworkCore;
using InventoryManagement.Models;

namespace InventoryManagement.Data
{
    public class InventoryApiDbContext : DbContext
    {

        public DbSet<Asset> Assets { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<User> Users { get; set; }

        // Other DbSet properties for additional entities
        public InventoryApiDbContext(DbContextOptions<InventoryApiDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Asset>().HasOne(a => a.collection).WithMany(c => c.Assets).HasForeignKey(a => a.CollectionId).IsRequired(false);
        }
    }
}