using Microsoft.AspNetCore.Mvc;
using seecreativa_backend.Classifications.Repositories;
using seecreativa_backend.Products.Models;
using seecreativa_backend.Products.Repositories;
using seecreativa_backend.Users.Attributes;
using seecreativa_backend.Utils.Attributes;


namespace seecreativa_backend.Products.Controllers {
    [ApiController]
    [Route("Api/[controller]")]
    public class ProductsController : Controller {
        private readonly IProductsRepository _productsRepository;
        private readonly IClassificationsRepository _classificationsRepository;

        public ProductsController(IProductsRepository productsRepository, IClassificationsRepository classificationsRepository) {
            _productsRepository = productsRepository;
            _classificationsRepository = classificationsRepository;
        }

        /// <summary>
        /// [User] Creates a new product.
        /// </summary>
        /// <param name="createDto">The data for the new product.</param>
        /// <returns>The newly created product.</returns>
        /// <response code="201">Returns the newly created product.</response>
        /// <response code="400">If the data is invalid.</response>
        /// <response code="400">If the product already exist.</response>
        /// <response code="404">If no classification with the given Id was found.</response>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ProductResponseDto>> Create([FromBody] ProductCreateDto createDto) {
            if ((await _classificationsRepository.GetByIdAsync(createDto.ClassificationId)) == null)
                return NotFound($"Classification with the Id {createDto.ClassificationId} not found");
            if ((await _productsRepository.GetByCodeAsync(createDto.Code)) != null)
                return BadRequest($"Product with the code {createDto.Code} already exist");
            var result = await _productsRepository.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result.ToResponse());
        }

        /// <summary>
        /// [User] Returns a list of all products.
        /// </summary>
        /// <returns>A list of all products.</returns>
        /// <response code="200">Returns a list of all products.</response>
        /// <response code="401">If the authentication token is invalid.</response>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProductWithClassificationResponseDto>>> ListBy(string? q) {
            var products = await _productsRepository.GetAllAsync(q);
            //var responseProducts = products.Select(u => u.ToResponse()).ToList();
            //return Ok(responseProducts);
            return Ok(products);
        }

        /// <summary>
        /// [User] Gets a product by Id.
        /// </summary>
        /// <param name="id">The Id of the product to get.</param>
        /// <returns>The product with the given Id.</returns>
        /// <response code="200">Returns the product with the given Id.</response>
        /// <response code="400">If the data is invalid.</response>
        /// <response code="404">If no product with the given Id was found.</response>
        /// <response code="401">If the authentication token is invalid.</response>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ProductWithClassificationResponseDto>> GetById([ValidateId] string id) {
            var product = await _productsRepository.GetProductByIdWithClassificationAsync(id);
            if (product == null) {
                return NotFound($"Product with the Id {id} not found");
            }
            return Ok(product);
        }

        /// <summary>
        /// [User] Updates a product by Id.
        /// </summary>
        /// <param name="id">The Id of the product to update.</param>
        /// <param name="updateDto">The data to update the product with.</param>
        /// <returns>The updated product.</returns>
        /// <response code="200">Returns the updated product.</response>
        /// <response code="400">If the data is invalid.</response>
        /// <response code="400">If the product already exist.</response>
        /// <response code="404">If no product with the given Id was found.</response>
        /// <response code="404">If no classification with the given Id was found.</response>
        /// <response code="401">If the authentication token is invalid.</response>
        [HttpPatch("{id}")]
        [Authorize]
        public async Task<ActionResult<ProductResponseDto>> UpdateById([ValidateId] string id, [FromBody] ProductUpdateDto updateDto) {
            if (updateDto.ClassificationId != null)
                if ((await _classificationsRepository.GetByIdAsync(updateDto.ClassificationId)) == null)
                    return NotFound($"Classification with the Id {updateDto.ClassificationId} not found");
            if (updateDto.Code != null)
                if ((await _productsRepository.GetByCodeAsync(updateDto.Code)) != null)
                    return BadRequest($"Product with the code {updateDto.Code} already exist");
            var result = await _productsRepository.UpdateByIdAsync(id, updateDto);
            if (result == null) return NotFound($"Product with the Id {id} not found");
            return Ok(result.ToResponse());
        }

        /// <summary>
        /// [User] Deletes a product by Id.
        /// </summary>
        /// <param name="id">The Id of the product to delete.</param>
        /// <returns>True if the product was deleted successfully, false otherwise.</returns>
        /// <response code="200">Returns true if the product was deleted successfully.</response>
        /// <response code="400">If the data is invalid.</response>
        /// <response code="404">If no product with the given Id was found.</response>
        /// <response code="401">If the authentication token is invalid.</response>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteById([ValidateId] string id) {
            var result = await _productsRepository.DeleteByIdAsync(id);
            if (!result) return NotFound($"Product with the Id {id} not found");
            return Ok(result);
        }

    }
}