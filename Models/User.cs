using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models
{
    public class User : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string OrgId { get; set; } = string.Empty;
        public string PicturePath { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, MinLength(6, ErrorMessage = "Enter at least 6 characters")]
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

        // Foreign key for the collections this user has access to
        public ICollection<Collection>? OwnedCollections { get; set; }
        //public List<Collection> OwnedCollections { get; set; }

        public User(NewUserRequest user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Password = "pass12345";
            OrgId = user.OrgId is null ? "" : user.OrgId;
            PicturePath = user.PicturePath is null ? "" : user.PicturePath;
            OwnedCollections = null;
        }

        public User()
        {

        }
    }
}

