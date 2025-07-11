using Application.Service;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal; 
using System.Threading.Tasks;

namespace projemaksut.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly IValidator<UserDto> validator;
        public UserController(IUserService userService, IMapper mapper, IValidator<UserDto> validator)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.validator = validator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            return await userService.GetUser(id);
        }

        [HttpPost("user")]
        public async Task<ActionResult<UserDto>> PostUser([FromBody] UserDto user, IValidator<UserDto> validator)
        {
            var validationResult = await validator.ValidateAsync(user);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            return await userService.CreateUser(user);
        }

        [HttpGet("users")]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            return await userService.GetUsers();
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            return await userService.DeleteUser(id);
        }
        [HttpPut("update/{id}")]
        public async Task<ActionResult<UserDto>> UpdateUser(int id, [FromBody] UserDto updateUser, IValidator<UserDto> validator)
        {
            var validationResult = await validator.ValidateAsync(updateUser);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            return await userService.UpdateUser(id, updateUser);
        }
    }
}
