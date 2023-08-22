using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InventoryManagement.Data;
using InventoryManagement.Models;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryManagement.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly InventoryApiDbContext _db;
        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public AuthController(InventoryApiDbContext db, IUserService userService, IConfiguration config)
        {
            _db = db;
            _userService = userService;
            _config = config;
        }



        [HttpPost("register")]
        public async Task<ActionResult<UserDtoResponse>> Register([FromBody] RegisterRequest request)
        {

            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return Problem("Missing details");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password, _config["SALT_HASH"]);

            var user = new User();
            user.Email = request.Email;
            user.Password = passwordHash;
            user.FirstName = user.FirstName;
            user.LastName = user.LastName;

            var dbUser = await _userService.AddUserAsync(user);

            if (dbUser is null)
            {
                return Problem("Something happened, contact admin.");
            }

            return Ok(new UserDtoResponse(dbUser));
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticateResponse>> Login([FromBody] AuthenticateRequest request)
        {
            if (string.IsNullOrEmpty(request.email) || string.IsNullOrEmpty(request.password))
            {
                return Problem();
            }

            var dbUser = await _db.Users.FirstOrDefaultAsync(x => x.Email == request.email);

            if (dbUser is null)
            {
                return BadRequest("Wrong Email/Password");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.password, dbUser.Password))
            {
                return BadRequest("Wrong Email/Password");
            }

            var token = new JwtSecurityTokenHandler().WriteToken(GenerateJwtToken(dbUser));

            if (token is null)
            {
                return Problem("Error logging in");
            }

            return Ok(new AuthenticateResponse(dbUser, token));
        }

        private JwtSecurityToken? GenerateJwtToken(User user)
        {

            var userClaims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var secret = _config["JWT_SECRET"];

            if (secret is null)
            {
                return null;
            }

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

            var token = new JwtSecurityToken(issuer: "InventoryManagement", audience: "InventoryManagement", expires: DateTime.Now.AddDays(1), claims: userClaims, signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha512));

            return token;
        }
    }

}

