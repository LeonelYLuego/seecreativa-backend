using Microsoft.AspNetCore.Mvc;
using seecreativa_backend.Clients.Models;
using seecreativa_backend.Clients.Repositories;
using seecreativa_backend.Users.Attributes;
using seecreativa_backend.Utils.Attributes;


namespace seecreativa_backend.Clients.Controllers {
    [ApiController]
    [Route("Api/[controller]")]
    public class ClientsController : Controller {
        private readonly IClientsRepository _clientsRepository;

        public ClientsController(IClientsRepository clientsRepository) {
            _clientsRepository = clientsRepository;
        }

        /// <summary>
        /// [User] Creates a new client.
        /// </summary>
        /// <param name="createDto">The data for the new client.</param>
        /// <returns>The newly created client.</returns>
        /// <response code="201">Returns the newly created client.</response>
        /// <response code="400">If the data is invalid.</response>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ClientResponseDto>> Create([FromBody] ClientCreateDto createDto) {
            var result = await _clientsRepository.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result.ToResponse());
        }

        /// <summary>
        /// [User] Returns a list of all clients.
        /// </summary>
        /// <returns>A list of all clients.</returns>
        /// <response code="200">Returns a list of all clients.</response>
        /// <response code="401">If the authentication token is invalid.</response>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ClientResponseDto>>> ListBy() {
            var clients = await _clientsRepository.GetAllAsync();
            var responseClients = clients.Select(u => u.ToResponse()).ToList();
            return Ok(responseClients);
        }

        /// <summary>
        /// [User] Gets a client by Id.
        /// </summary>
        /// <param name="id">The Id of the client to get.</param>
        /// <returns>The client with the given Id.</returns>
        /// <response code="200">Returns the client with the given Id.</response>
        /// <response code="400">If the data is invalid.</response>
        /// <response code="404">If no client with the given Id was found.</response>
        /// <response code="401">If the authentication token is invalid.</response>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ClientResponseDto>> GetById([ValidateId] string id) {
            var client = await _clientsRepository.GetByIdAsync(id);
            if (client == null) {
                return NotFound($"Client with the Id {id} not found");
            }
            return Ok(client.ToResponse());
        }

        /// <summary>
        /// [User] Updates a client by Id.
        /// </summary>
        /// <param name="id">The Id of the client to update.</param>
        /// <param name="updateDto">The data to update the client with.</param>
        /// <returns>The updated client.</returns>
        /// <response code="200">Returns the updated client.</response>
        /// <response code="400">If the data is invalid.</response>
        /// <response code="404">If no client with the given Id was found.</response>
        /// <response code="401">If the authentication token is invalid.</response>
        [HttpPatch("{id}")]
        [Authorize]
        public async Task<ActionResult<ClientResponseDto>> UpdateById([ValidateId] string id, [FromBody] ClientUpdateDto updateDto) {
            var result = await _clientsRepository.UpdateByIdAsync(id, updateDto);
            if (result == null) return NotFound($"Client with the Id {id} not found");
            return Ok(result.ToResponse());
        }

        /// <summary>
        /// [User] Deletes a client by Id.
        /// </summary>
        /// <param name="id">The Id of the client to delete.</param>
        /// <returns>True if the client was deleted successfully, false otherwise.</returns>
        /// <response code="200">Returns true if the client was deleted successfully.</response>
        /// <response code="400">If the data is invalid.</response>
        /// <response code="404">If no client with the given Id was found.</response>
        /// <response code="401">If the authentication token is invalid.</response>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteById([ValidateId] string id) {
            var result = await _clientsRepository.DeleteByIdAsync(id);
            if (!result) return NotFound($"Client with the Id {id} not found");
            return Ok(result);
        }

    }
}