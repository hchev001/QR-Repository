using InventoryManagement.Models;
using Microsoft.AspNetCore.Identity;

namespace InventoryManagement.Data.repositories
{
	public interface IUserRepository
	{
		Task<IdentityResult> CreateUserAsync(User user, string password);
	}
	public class UserRepository:IUserRepository
	{

		private readonly UserManager<User> _userManager;

		public UserRepository(UserManager<User> userManager)
		{
			_userManager = userManager;
		}

        public async Task<IdentityResult> CreateUserAsync(User user, string password)
        {
			return await _userManager.CreateAsync(user, password);
        }
    }
}

