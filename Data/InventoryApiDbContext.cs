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

        public override int SaveChanges()
        {
            AddTimeStamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            AddTimeStamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Asset>().HasOne(a => a.collection).WithMany(c => c.Assets).HasForeignKey(a => a.CollectionId).IsRequired(false);
        }

        private void AddTimeStamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow;

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedAt = now;
                }
                ((BaseEntity)entity.Entity).UpdatedAt = now;
            }
        }
    }
}