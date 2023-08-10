
using InventoryManagement.Data;
using InventoryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Services
{
	public class CollectionService:ICollectionService
	{
        private readonly InventoryApiDbContext _db;

		public CollectionService(InventoryApiDbContext db)
		{
            _db = db;
		}

        public async Task<Collection> AddCollectionAsync(Collection collection)
        {
            try
            {
                await _db.Collections.AddAsync(collection);
                _db.SaveChanges();
                return await _db.Collections.FindAsync(collection.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteCollectionAsync(Collection collection)
        {
            try
            {
                var dbCollection = await _db.Collections.FindAsync(collection.Id);

                if (collection is null)
                {
                    return (false, "Asset not found");
                }

                _db.Collections.Remove(collection);
                _db.SaveChanges();

                return (true, "Asset got deleted");
            }
            catch (Exception ex)
            {
                return (false, $"Error occured: Error message: {ex.Message}");
            }
        }

        public async Task<Collection> GetCollectionAsync(Guid id)
        {
            try
            {
                return await _db.Collections.FindAsync(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Collection>?> GetCollectionsAsync()
        {
            try
            {
                return await _db.Collections.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Collection> UpdateCollectionAsync(Collection collection)
        {
            try
            {
                _db.Entry(collection).State = EntityState.Modified;
                _db.SaveChanges();
                return collection;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

