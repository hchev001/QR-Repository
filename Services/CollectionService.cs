
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

        public Task<Collection> AddCollection()
        {
            throw new NotImplementedException();
        }

        public async Task<(bool, string)> DeleteCollection(Collection collection)
        {
            try
            {
                var dbCollection = await _db.Collections.FindAsync(collection.Id);

                if (collection is null)
                {
                    return (false, "Asset not found");
                }

                _db.Collections.Remove(collection);
                await _db.SaveChangesAsync();

                return (true, "Asset got deleted");
            }
            catch (Exception ex)
            {
                return (false, $"Error occured: Error message: {ex.Message}");
            }
        }

        public async Task<Collection> GetCollection(Guid id)
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

        public async Task<Collection> UpdateCollection(Collection collection)
        {
            try
            {
                _db.Entry(collection).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return collection;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

