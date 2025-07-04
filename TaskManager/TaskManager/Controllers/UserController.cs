using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TaskManager.Models;
using TaskManager.DTOs;
using TaskManager.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public UserController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserGetDTOs>> GetUserAsync(int id)
        {
            var user = await _userService.GetUserAsync(id);

            if (user == null)
                return BadRequest();

            UserGetDTOs userDto = new UserGetDTOs
            {
                Id = user.Id,
                UserName = user.UserName,
                Role = user.Role,
            };

            return Ok(userDto);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<UserGetDTOs>> UpdateUserAsync(UserUpdateDTOs dto)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var existingUser = await _userService.GetUserAsync(userId);

            var user = await _userService.UpdateUserAsync(userId, dto);

            if (user == null)
            {
                return BadRequest();
            }

            UserGetDTOs userDtos = new UserGetDTOs
            {
                Id = user.Id,
                UserName = user.UserName,
                Role = user.Role,
            };

            if (user.UserName == existingUser.UserName)
            {
                return Ok(userDtos);
            }
            else
            {
                var new_token = _authService.CreateToken(user);

                return Ok(new { userDtos, new_token });
            }
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<UserGetDTOs>>> GetAllUsersAsync()
        {
            List<User> users = await _userService.GetAllUsersAsync();

            var userDtos = users.Select(user => new UserGetDTOs
            {
                Id = user.Id,
                UserName = user.UserName,
                Role = user.Role
            }).ToList();

            return Ok(userDtos);
        }
    }
}
