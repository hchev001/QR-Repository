using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace InventoryManagement.Models
{
    public class Asset
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public string? Tags { get; set; }
        public string? Description { get; set; }

        //Foreign key for the related collection 
        public Guid? CollectionId { get; set; }
        // navigation property for the related collection 
        public Collection? collection { get; set; }
    }

    
}

