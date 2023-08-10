using System;
using InventoryManagement.Models;

namespace InventoryManagement.Services
{
	public interface IUserService
	{
		Task<List<User>> GetUsersAsync();
		Task<User> GetUserAsync(Guid id);
		Task<User> AddUserAsync(User user);
		Task<User> UpdateUserAsync(User user);
		Task<(bool, string)> DeleteUserAsync(User user);
	}
}

