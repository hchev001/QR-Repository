using System.Security.Claims;
using InventoryManagement.Models;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryManagement.Controllers
{
    [Route("api/[controller]")]
    public class CollectionsController : ControllerBase
    {
        private readonly ICollectionService _collectionService;
        private readonly IUserService _userService;

        public CollectionsController(ICollectionService collectionService, IUserService userService)
        {
            _collectionService = collectionService;
            _userService = userService;

        }

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> GetCollections()
        {
            var collections = await _collectionService.GetCollectionsAsync();

            if (collections is null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No collections in the database.");
            }

            return StatusCode(StatusCodes.Status200OK, collections);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCollection(Guid id)
        {
            Collection collection = await _collectionService.GetCollectionAsync(id);

            if (collection is null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No collection found for id:{id}");
            }

            return StatusCode(StatusCodes.Status200OK, collection);
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Collection>> AddCollection([FromBody]CollectionRequest collection)
        {
            if (string.IsNullOrEmpty(collection.Name))
            {
                return Problem("Missing name");
            }

            // get the id for the current context user
            var currentUser_id = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (currentUser_id is null) {
                return Problem("Bad Token");
            }

            var user = await _userService.GetUserAsync(new Guid(currentUser_id.Value));

            if (user is null) {
                return Problem("Bad Token");
            }

            var _collection = new Collection
            {
                Name = collection.Name,             // and associate that user to this collection
                Owners = new List<User> { user }
            
            };

            var dbCollection = await _collectionService.AddCollectionAsync(_collection);

            if (dbCollection is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction("GetCollection", new { id = _collection.Id }, collection);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCollection(Guid id, Collection collection)
        {
            if (id != collection.Id)
            {
                return BadRequest();
            }

            Collection dbCollection = await _collectionService.UpdateCollectionAsync(collection);

            if (dbCollection is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollection(Guid id)
        {
            var collection = await _collectionService.GetCollectionAsync(id);
            (bool status, string message) = await _collectionService.DeleteCollectionAsync(collection);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return StatusCode(StatusCodes.Status200OK, collection);
        }
    }
}

