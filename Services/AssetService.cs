using InventoryManagement.Data;
using InventoryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Services
{
	public class AssetService: IAssetService
	{
        private readonly InventoryApiDbContext _db;

		public AssetService(InventoryApiDbContext db)
		{
            _db = db;
		}

        public async Task<Asset> AddAssetAsync(Asset asset)
        {
            try
            {
                await _db.Assets.AddAsync(asset);
                _db.SaveChanges();
                return await _db.Assets.FindAsync(asset.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteAssetAsync(Asset asset)
        {
            try
            {
                var dbAsset = await _db.Assets.FindAsync(asset.Id);

                if (dbAsset is null)
                {
                    return (false, "Asset not found");
                }

                _db.Assets.Remove(asset);
                _db.SaveChanges();

                return (true, "Asset got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error message: {ex.Message}");
            }
        }

        public async Task<Asset> GetAssetAsync(Guid id)
        {
            try
            {
                return await _db.Assets.FindAsync(id);
            } catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Asset>?> GetAssetsAsync()
        {
            try
            {
                return await _db.Assets.ToListAsync();
            } catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Asset> UpdateAssetAsync(Asset asset)
        {
            try
            {
                _db.Entry(asset).State = EntityState.Modified;
                _db.SaveChanges();
                return asset;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

