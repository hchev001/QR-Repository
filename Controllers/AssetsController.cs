
using InventoryManagement.Models;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Mvc;


namespace InventoryManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetController : ControllerBase
    {
        private readonly IAssetService _assetService;

        public AssetController(IAssetService assetService)
        {
            _assetService = assetService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAssets()
        {
            var assets = await _assetService.GetAssetsAsync();

            if (assets is null) {
                return StatusCode(StatusCodes.Status204NoContent, "No assets in the database.");
            }

            return StatusCode(StatusCodes.Status200OK, assets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsset(Guid id)
        {
            Asset asset = await _assetService.GetAssetAsync(id);

            if (asset is null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No asset found for id:{id}");
            }

            return StatusCode(StatusCodes.Status200OK, asset);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<Asset>> AddAsset(Asset asset)
        {
            var dbAsset = await _assetService.AddAssetAsync(asset);

            if (dbAsset is null) {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction("GetAsset", new { id = asset.Id }, asset);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsset(Guid id, Asset asset)
        {
            if (id != asset.Id)
            {
                return BadRequest();
            }

            Asset dbAsset = await _assetService.UpdateAssetAsync(asset);

            if (dbAsset is null) {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsset(Guid id)
        {
            var asset = await _assetService.GetAssetAsync(id);
            (bool status, string message) = await _assetService.DeleteAssetAsync(asset);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return StatusCode(StatusCodes.Status200OK, asset);
        }
    }
}

