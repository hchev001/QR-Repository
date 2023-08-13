namespace InventoryManagement.Models
{
    public class UserDtoRequest
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }

    public class UserDtoResponse
    {
        public Guid? Id { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PicturePath { get; set; }
        public string? OrgId { get; set; }
        public string? Token { get; set; }

        public UserDtoResponse(User user, string token)
        {
            this.Token = token;
            this.OrgId = user.OrgId;
            this.PicturePath = user.PicturePath;
            this.LastName = user.LastName;
            this.FirstName = user.FirstName;
            this.Email = user.Email;
            this.Id = user.Id;
        }

        public UserDtoResponse(User user)
        {
            this.OrgId = user.OrgId;
            this.PicturePath = user.PicturePath;
            this.LastName = user.LastName;
            this.FirstName = user.FirstName;
            this.Email = user.Email;
            this.Id = user.Id;
        }
    }
}

