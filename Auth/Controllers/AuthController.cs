using Domain.Requests;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUsuarios()
        {
            try
            {
                var userList = _userService.GetAllUsers();
                if (userList == null)
                {
                    return NotFound();
                }
                return Ok(userList);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return StatusCode(500, "Internal server error, please contact support");
            }
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateUserRequest createUserRequest)
        {
            bool validate = _userService.IsUnique(createUserRequest.UserName);

            if (!validate)
            {
                return StatusCode(400, "Username is already used.");
            }

            if (!ModelState.IsValid || createUserRequest == null)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var res = await _userService.Register(createUserRequest);
                if (res == null)
                    return StatusCode(500, "Internal server error, please contact support");

                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return StatusCode(500, "Internal server error, please contact support");
            }
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest loginUserRequest)
        {
            if (!ModelState.IsValid || loginUserRequest == null)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var res = await _userService.Login(loginUserRequest);
                if (res.User == null || string.IsNullOrEmpty(res.Token))
                    return StatusCode(400, "Login not successful");

                return Ok(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return StatusCode(500, "Internal server error, please contact support");
            }
        }
    }
}
