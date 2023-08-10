using InventoryManagement.Services;
using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryManagement.Controllers
{
    
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsersAsync();

            if (users is null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No users in the database.");
            }

            return StatusCode(StatusCodes.Status200OK, users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            User user = await _userService.GetUserAsync(id);

            if (user is null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No user found for id:{id}");
            }

            return StatusCode(StatusCodes.Status200OK, user);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            var dbUser = await _userService.AddUserAsync(user);

            if (dbUser is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            User dbUser = await _userService.UpdateUserAsync(user);

            if (dbUser is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userService.GetUserAsync(id);
            (bool status, string message) = await _userService.DeleteUserAsync(user);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return StatusCode(StatusCodes.Status200OK, user);
        }
    }
}

