using Microsoft.AspNetCore.Mvc;
using seecreativa_backend.Prices.Models;
using seecreativa_backend.Prices.Repositories;
using seecreativa_backend.Users.Attributes;
using seecreativa_backend.Utils.Attributes;


namespace seecreativa_backend.Prices.Controllers {
    [ApiController]
    [Route("Api/[controller]")]
    public class PricesController : Controller {
        private readonly IPricesRepository _pricesRepository;

        public PricesController(IPricesRepository pricesRepository) {
            _pricesRepository = pricesRepository;
        }

        /// <summary>
        /// [User] Creates a new price.
        /// </summary>
        /// <param name="createDto">The data for the new price.</param>
        /// <returns>The newly created price.</returns>
        /// <response code="201">Returns the newly created price.</response>
        /// <response code="400">If the data is invalid.</response>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PriceResponseDto>> Create([FromBody] PriceCreateDto createDto) {
            var result = await _pricesRepository.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result.ToResponse());
        }

        /// <summary>
        /// [User] Returns a list of all prices.
        /// </summary>
        /// <returns>A list of all prices.</returns>
        /// <response code="200">Returns a list of all prices.</response>
        /// <response code="401">If the authentication token is invalid.</response>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PriceResponseDto>>> ListBy() {
            var prices = await _pricesRepository.GetAllAsync();
            var responsePrices = prices.Select(u => u.ToResponse()).ToList();
            return Ok(responsePrices);
        }

        /// <summary>
        /// [User] Gets a price by Id.
        /// </summary>
        /// <param name="id">The Id of the price to get.</param>
        /// <returns>The price with the given Id.</returns>
        /// <response code="200">Returns the price with the given Id.</response>
        /// <response code="400">If the data is invalid.</response>
        /// <response code="404">If no price with the given Id was found.</response>
        /// <response code="401">If the authentication token is invalid.</response>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<PriceResponseDto>> GetById([ValidateId] string id) {
            var price = await _pricesRepository.GetByIdAsync(id);
            if (price == null) {
                return NotFound($"Price with the Id {id} not found");
            }
            return Ok(price.ToResponse());
        }

        /// <summary>
        /// [User] Updates a price by Id.
        /// </summary>
        /// <param name="id">The Id of the price to update.</param>
        /// <param name="updateDto">The data to update the price with.</param>
        /// <returns>The updated price.</returns>
        /// <response code="200">Returns the updated price.</response>
        /// <response code="400">If the data is invalid.</response>
        /// <response code="404">If no price with the given Id was found.</response>
        /// <response code="401">If the authentication token is invalid.</response>
        [HttpPatch("{id}")]
        [Authorize]
        public async Task<ActionResult<PriceResponseDto>> UpdateById([ValidateId] string id, [FromBody] PriceUpdateDto updateDto) {
            var result = await _pricesRepository.UpdateByIdAsync(id, updateDto);
            if (result == null) return NotFound($"Price with the Id {id} not found");
            return Ok(result.ToResponse());
        }

        /// <summary>
        /// [User] Deletes a price by Id.
        /// </summary>
        /// <param name="id">The Id of the price to delete.</param>
        /// <returns>True if the price was deleted successfully, false otherwise.</returns>
        /// <response code="200">Returns true if the price was deleted successfully.</response>
        /// <response code="400">If the data is invalid.</response>
        /// <response code="404">If no price with the given Id was found.</response>
        /// <response code="401">If the authentication token is invalid.</response>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteById([ValidateId] string id) {
            var result = await _pricesRepository.DeleteByIdAsync(id);
            if (!result) return NotFound($"Price with the Id {id} not found");
            return Ok(result);
        }

    }
}