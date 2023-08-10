using System;
using InventoryManagement.Models;
namespace InventoryManagement.Services
{
	public interface IAssetService
	{
		Task<List<Asset>?> GetAssetsAsync();
		Task<Asset> GetAssetAsync(Guid id);
		Task<Asset> AddAssetAsync(Asset asset);
		Task<Asset> UpdateAssetAsync(Asset asset);
		Task<(bool, string)> DeleteAssetAsync(Asset asset);

	}
}

