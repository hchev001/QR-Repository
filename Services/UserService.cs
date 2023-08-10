using System;
using InventoryManagement.Data;
using InventoryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Services
{
	public class UserService:IUserService
	{
        private readonly InventoryApiDbContext _db;
		public UserService(InventoryApiDbContext db)
		{
            _db = db;
		}

        public async Task<User> AddUserAsync(User user)
        {
            try
            {
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
                return await _db.Users.FindAsync(user.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteUserAsync(User user)
        {
            try
            {
                var dbUser = await _db.Users.FindAsync(user.Id);

                 if (dbUser is null)
                {
                    return (false, "User not found");
                }

                _db.Users.Remove(user);
                await _db.SaveChangesAsync();

                return (true, "User deleted");
            }
            catch (Exception ex)
            {
                return (false, $"Error deleting user: ${ex.Message}");
            }
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            try
            {
                return await _db.Users.FindAsync(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<User>> GetUsersAsync()
        {
            try
            {
                return await _db.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            try
            {
                _db.Entry(user).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

