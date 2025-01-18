using AccountManagementServer.Application.Interface;
using AccountManagementServer.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CardsProductsServer.Presentasions.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, User user)
        {
            User? userToUpdata = await _userService.Update(id, user);
            if (userToUpdata == null)
            {
                return NotFound();
            }
            return NoContent();
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

    }
}
