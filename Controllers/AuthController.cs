using InventoryManagement.Data;
using InventoryManagement.Models;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryManagement.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly InventoryApiDbContext _db;
        private readonly IUserService _userService;

        public AuthController(InventoryApiDbContext db, IUserService userService)
        {
            _db = db;
            _userService = userService;
        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(UserDto request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return Problem("Missing details");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User();
            user.Email = request.Email;
            user.Password = passwordHash;
            user.FirstName = user.FirstName;
            user.LastName = user.LastName;

            var dbUser = await _userService.AddUserAsync(user);

            if (dbUser is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(dbUser);
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(UserDto request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return Problem("Missing details");
            }

            //find the user in the database
            if (request.Email != request.Email)
            {
                return BadRequest("User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, request.Password))
            {
                return BadRequest("Wrong password");
            }

            return Ok();
        }
    }
}

