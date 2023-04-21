using Microsoft.AspNetCore.Mvc;
using seecreativa_backend.Users.Models;
using seecreativa_backend.Users.Repositories;

namespace seecreativa_backend.Users.Controllers {
    [ApiController]
    [Route("Api/[controller]")]
    public class AuthController : Controller {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository) {
            _authRepository = authRepository;
        }

        /// <summary>
        /// [All] Logs in a user with the provided credentials.
        /// </summary>
        /// <param name="logInDto">The data for the user to log in.</param>
        /// <returns>The authenticated user information and token.</returns>
        /// <response code="200">Returns the authenticated user information and token.</response>
        /// <response code="401">If the provided credentials are invalid.</response>
        [HttpPost]
        public async Task<ActionResult<AuthResponseDto>> LogIn([FromBody] AuthLogInDto logInDto) {
            var result = await _authRepository.LogIn(logInDto);
            if (result == null) return Unauthorized();
            return Ok(result);
        }
    }
}
