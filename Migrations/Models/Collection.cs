using System.ComponentModel.DataAnnotations;


namespace InventoryManagement.Models
{
    public class Collection:BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        //Foreign key for owners of this collection
        //public List<User> Owners { get; set; }
        public ICollection<User> Owners { get; set; }

        // Foreign key for the related assets
        //public List<Asset> Assets { get; set; }
        public ICollection<Asset> Assets { get; set; }
    }
}

