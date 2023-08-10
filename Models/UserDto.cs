

namespace InventoryManagement.Models
{
	public class UserDto
	{
        public required Guid Id { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}

