using System;
using InventoryManagement.Models;

namespace InventoryManagement.Services
{
	public interface ICollectionService
	{
		Task<List<Collection>?> GetCollectionsAsync();
		Task<Collection> GetCollection(Guid id);
		Task<Collection> AddCollection();
		Task<Collection> UpdateCollection(Collection collection);
		Task<(bool, string)> DeleteCollection(Collection collection);
	}
}

