using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models
{
    public class User:BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string OrgId { get; set; } = string.Empty;
        public string picturePath { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        // Foreign key for the collections this user has access to
        public ICollection<Collection> OwnedCollections { get; set; }
        //public List<Collection> OwnedCollections { get; set; }
    }
}

