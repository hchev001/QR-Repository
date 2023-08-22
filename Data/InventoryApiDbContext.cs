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

            builder.Entity<User>().HasMany(s => s.OwnedCollections).WithMany(c => c.Owners).UsingEntity(j =>
            {
                j.ToTable("CollectionUser");
                j.HasData(
                    new { OwnedCollectionsId = new Guid("b257b0a2-acff-4633-8c46-4f3c5d712814"), OwnersId = new Guid("351ec5aa-4200-4c6d-aedd-4b3de561651a") }
                );
            });

            builder.Entity<User>().HasData(SeedUsers());
            builder.Entity<Collection>().HasData(SeedCollections());
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

        private static User[] SeedUsers()
        {
            var initialUsers = new User[1];

            var user = new User();
            user.Id = new Guid("351ec5aa-4200-4c6d-aedd-4b3de561651a");
            user.Email = "admin@admin.com";
            user.PicturePath = "";
            user.FirstName = "Ronald";
            user.LastName = "McDonald";
            user.Password = "$2a$11$t6Mo/hupJrkBPTpkcbHnmeuPXFj9TdW4.8nFa2cuc2KQFUYp/Ha2C"; // hashed pass12345
            user.Role = "Admin";
            user.OrgId = "P0995800";

            initialUsers[0] = user;

            return initialUsers;

        }

        private static Collection[] SeedCollections()
        {
            var initialCollections = new Collection[1];
            var col = new Collection();

            col.Id = new Guid("b257b0a2-acff-4633-8c46-4f3c5d712814");
            col.CreatedAt = new DateTime();
            col.UpdatedAt = new DateTime();

            initialCollections[0] = col;
            return initialCollections;
        }

    }
}