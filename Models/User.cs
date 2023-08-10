using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Foreign key for the collections this user has access to
         public ICollection<Collection> OwnedCollections { get; set; }
        //public List<Collection> OwnedCollections { get; set; }
    }
}

