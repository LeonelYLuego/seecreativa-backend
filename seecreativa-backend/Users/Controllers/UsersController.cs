using Microsoft.AspNetCore.Mvc;
using seecreativa_backend.Users.Models;
using seecreativa_backend.Users.Repositories;
using seecreativa_backend.Utils.Attributes;

namespace seecreativa_backend.Users.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUsersRepository _usersRepository;

        public UsersController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="createDto">The data for the new user.</param>
        /// <returns>The newly created user.</returns>
        /// <response code="201">Returns the newly created user.</response>
        /// <response code="400">If the data is invalid.</response>
        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> Create([FromBody] UserCreateDto createDto)
        {
            var result = await _usersRepository.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result.ToResponse());
        }

        /// <summary>
        /// Returns a list of all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        /// <response code="200">Returns a list of all users.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> ListBy()
        {
            var users = await _usersRepository.GetAllAsync();
            var responseUsers = users.Select(u => u.ToResponse()).ToList();
            return Ok(responseUsers);
        }

        /// <summary>
        /// Gets a user by Id.
        /// </summary>
        /// <param name="id">The Id of the user to get.</param>
        /// <returns>The user with the given Id.</returns>
        /// <response code="200">Returns the user with the given Id.</response>
        /// <response code="404">If no user with the given Id was found.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponseDto>> GetById([ValidateId] string id)
        {
            var user = await _usersRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound($"User with the Id {id} not found");
            }
            return Ok(user.ToResponse());
        }

        /// <summary>
        /// Updates a user by Id.
        /// </summary>
        /// <param name="id">The Id of the user to update.</param>
        /// <param name="updateDto">The data to update the user with.</param>
        /// <returns>The updated user.</returns>
        /// <response code="200">Returns the updated user.</response>
        /// <response code="400">If the data is invalid.</response>
        /// <response code="404">If no user with the given Id was found.</response>
        [HttpPatch("{id}")]
        public async Task<ActionResult<UserResponseDto>> UpdateById([ValidateId] string id, [FromBody] UserUpdateDto updateDto)
        {
            var result = await _usersRepository.UpdateByIdAsync(id, updateDto);
            if (result == null) return NotFound($"User with the Id {id} not found");
            return Ok(result.ToResponse());
        }

        /// <summary>
        /// Deletes a user by Id.
        /// </summary>
        /// <param name="id">The Id of the user to delete.</param>
        /// <returns>True if the user was deleted successfully, false otherwise.</returns>
        /// <response code="200">Returns true if the user was deleted successfully.</response>
        /// <response code="404">If no user with the given Id was found.</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteById([ValidateId] string id)
        {
            var result = await _usersRepository.DeleteByIdAsync(id);
            if (!result) return NotFound($"User with the Id {id} not found");
            return Ok(result);
        }

    }
}
