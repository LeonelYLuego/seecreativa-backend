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

        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> Create([FromBody] UserCreateDto createDto)
        {
            var user = createDto.ToUser();
            var result = await _usersRepository.CreateAsync(user);
            return Ok(result.ToResponse());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> ListBy()
        {
            var users = await _usersRepository.GetAllAsync();
            var responseUsers = users.Select(u => u.ToResponse()).ToList();
            return Ok(responseUsers);
        }

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

        [HttpPatch("{id}")]
        public async Task<ActionResult<UserResponseDto>> UpdateById([ValidateId] string id, [FromBody] UserUpdateDto updateDto)
        {
            var user = updateDto.ToUser(id);
            var result = await _usersRepository.UpdateAsync(user);
            if (result == null) return NotFound($"User with the Id {id} not found");
            return Ok(result.ToResponse());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteById([ValidateId] string id)
        {
            var result = await _usersRepository.DeleteAsync(id);
            if (!result) return NotFound($"User with the Id {id} not found");
            return Ok(result);
        }
    }
}
