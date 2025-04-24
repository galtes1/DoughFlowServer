using AccountManagementServer.Application.Interface;
using AccountManagementServer.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountManagementServer.Presentasions.Controllers
{
    [Route("[controller]")]
   
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public UsersController(IUserService userService, IAuthService auth)
        {
            _userService = userService;
            _authService = auth;
        }


      

        [HttpPost("signUp")]
        public async Task<IActionResult> SignUp([FromBody] User user)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            User? result = await _userService.Register(user);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

   

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel login)
        {
            string? result = await _userService.Login(login);
            if (result == null)
            {
                return Unauthorized();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            User? findUser = await _userService.GetOne(id);
            if (findUser == null)
            {
                return NotFound();
            }
            return Ok(findUser);
        }       


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            User? userToRemove = await _userService.Delete(id);
            if (userToRemove == null)
            {
                return NotFound();
            }
            return Created();
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateUserRequest request)
        {
            var updatedUser = await _userService.UpdateUserAsync(id, request.CurrentPassword, new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                IsBusiness = request.IsBusiness
            });

            if (updatedUser == null)
            {
                return BadRequest("Update failed. Check current password if changing password.");
            }
            var newToken = _authService.GenerateToken(updatedUser);
            return Ok(new { token = newToken });
        }

  
        [HttpPost("refresh-token")]
        public IActionResult RefreshToken()
        {
            var identity = HttpContext.User;
            var userId = int.Parse(identity.FindFirst("UserId").Value);
            var isBusiness = bool.Parse(identity.FindFirst("IsBusiness").Value);

            var user = new User { UserId = userId, IsBusiness = isBusiness };
            var newToken = _authService.GenerateToken(user);

            return Ok(new { token = newToken });
        }

    }
}
