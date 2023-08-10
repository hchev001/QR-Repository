using System;
using InventoryManagement.Models;

namespace InventoryManagement.Services
{
	public interface ICollectionService
	{
		Task<List<Collection>?> GetCollectionsAsync();
		Task<Collection> GetCollectionAsync(Guid id);
		Task<Collection> AddCollectionAsync(Collection collection);
		Task<Collection> UpdateCollectionAsync(Collection collection);
		Task<(bool, string)> DeleteCollectionAsync(Collection collection);
	}
}

