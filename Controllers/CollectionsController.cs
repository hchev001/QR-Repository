using InventoryManagement.Models;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryManagement.Controllers
{
    [Route("api/[controller]")]
    public class CollectionsController : ControllerBase
    {
        private readonly ICollectionService _collectionService;

        public CollectionsController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
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

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<Collection>> AddCollection(Collection collection)
        {
            var dbCollection = await _collectionService.AddCollectionAsync(collection);

            if (dbCollection is null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction("GetCollection", new { id = collection.Id }, collection);
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
}

