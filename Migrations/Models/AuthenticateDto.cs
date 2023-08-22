namespace InventoryManagement.Models
{
    public class AuthenticateRequest
    {
        public string? email { get; set; }
        public string? password { get; set; }

    }

    public class AuthenticateResponse
    {
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PicturePath { get; set; }
        public string? OrgId { get; set; }
        public string? Token { get; set; }

        public AuthenticateResponse(User user, string Token)
        {
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            PicturePath = user.PicturePath;
            OrgId = user.OrgId;
            this.Token = Token;
        }
    }

    public class RegisterRequest
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PicturePath { get; set; }
        public string? OrgId { get; set; }
    }
}