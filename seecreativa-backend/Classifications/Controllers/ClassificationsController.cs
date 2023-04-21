using Microsoft.AspNetCore.Mvc;
using seecreativa_backend.Classifications.Models;
using seecreativa_backend.Classifications.Repositories;
using seecreativa_backend.Users.Attributes;
using seecreativa_backend.Utils.Attributes;


namespace seecreativa_backend.Classifications.Controllers {
    [ApiController]
    [Route("Api/[controller]")]
    public class ClassificationsController : Controller {
        private readonly IClassificationsRepository _classificationsRepository;

        public ClassificationsController(IClassificationsRepository classificationsRepository) {
            _classificationsRepository = classificationsRepository;
        }

        /// <summary>
        /// [User] Creates a new classification.
        /// </summary>
        /// <param name="createDto">The data for the new classification.</param>
        /// <returns>The newly created classification.</returns>
        /// <response code="201">Returns the newly created classification.</response>
        /// <response code="400">If the data is invalid.</response>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ClassificationResponseDto>> Create([FromBody] ClassificationCreateDto createDto) {
            var result = await _classificationsRepository.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result.ToResponse());
        }

        /// <summary>
        /// [User] Returns a list of all classifications.
        /// </summary>
        /// <returns>A list of all classifications.</returns>
        /// <response code="200">Returns a list of all classifications.</response>
        /// <response code="401">If the authentication token is invalid.</response>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ClassificationResponseDto>>> ListBy() {
            var classifications = await _classificationsRepository.GetAllAsync();
            var responseClassifications = classifications.Select(u => u.ToResponse()).ToList();
            return Ok(responseClassifications);
        }

        /// <summary>
        /// [User] Gets a classification by Id.
        /// </summary>
        /// <param name="id">The Id of the classification to get.</param>
        /// <returns>The classification with the given Id.</returns>
        /// <response code="200">Returns the classification with the given Id.</response>
        /// <response code="400">If the data is invalid.</response>
        /// <response code="404">If no classification with the given Id was found.</response>
        /// <response code="401">If the authentication token is invalid.</response>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ClassificationResponseDto>> GetById([ValidateId] string id) {
            var classification = await _classificationsRepository.GetByIdAsync(id);
            if (classification == null) {
                return NotFound($"Classification with the Id {id} not found");
            }
            return Ok(classification.ToResponse());
        }

        /// <summary>
        /// [User] Updates a classification by Id.
        /// </summary>
        /// <param name="id">The Id of the classification to update.</param>
        /// <param name="updateDto">The data to update the classification with.</param>
        /// <returns>The updated classification.</returns>
        /// <response code="200">Returns the updated classification.</response>
        /// <response code="400">If the data is invalid.</response>
        /// <response code="404">If no classification with the given Id was found.</response>
        /// <response code="401">If the authentication token is invalid.</response>
        [HttpPatch("{id}")]
        [Authorize]
        public async Task<ActionResult<ClassificationResponseDto>> UpdateById([ValidateId] string id, [FromBody] ClassificationUpdateDto updateDto) {
            var result = await _classificationsRepository.UpdateByIdAsync(id, updateDto);
            if (result == null) return NotFound($"Classification with the Id {id} not found");
            return Ok(result.ToResponse());
        }

        /// <summary>
        /// [User] Deletes a classification by Id.
        /// </summary>
        /// <param name="id">The Id of the classification to delete.</param>
        /// <returns>True if the classification was deleted successfully, false otherwise.</returns>
        /// <response code="200">Returns true if the classification was deleted successfully.</response>
        /// <response code="400">If the data is invalid.</response>
        /// <response code="404">If no classification with the given Id was found.</response>
        /// <response code="401">If the authentication token is invalid.</response>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteById([ValidateId] string id) {
            var result = await _classificationsRepository.DeleteByIdAsync(id);
            if (!result) return NotFound($"Classification with the Id {id} not found");
            return Ok(result);
        }

    }
}