using Application.Service;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using Infrastructure.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Net;
using System.Threading.Tasks;
using ValidationException = Infrastructure.Exception.ValidationException;


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

        [HttpGet("active/users")]

        public async Task<ActionResult<List<UserDto>>> GetActiveUsers()
        {
            return await userService.GetActiveUser();

        }

        [HttpGet("users")]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        { 
            return await userService.GetUsers();
        } 


        [HttpGet("filtre")]
        public async Task<ActionResult<List<UserDto>>> FiltreUsers([FromQuery]int? id ,[FromQuery]string? name, [FromQuery] string? surname , [FromQuery] string? email)
        {
           return await userService.FiltreUsers(id,name,surname,email);

        }

        [HttpPost("user")]
        public async Task<ActionResult<UserDto>> PostUser([FromBody] UserDto user,IValidator<UserDto> validator)
        {
            var validationResult = await validator.ValidateAsync(user);
            string errorMessage = string.Join(", ", validationResult.Errors);
            if (validationResult != null) {
                throw new ValidationException(errorMessage);
            
            }
            return await userService.CreateUser(user);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await  userService.GetUser(id);
            await userService.DeleteUser(id);
            if (user==null)
            {
                throw new NotFoundException($"User with ID {id} not found");

            }
            return Ok();
        }
        [HttpPut("update/{id}")]
        public async Task<ActionResult<UserDto>> UpdateUser(int id, [FromBody] UserDto updateUser, IValidator<UserDto> validator)
        {
            var validationResult = await validator.ValidateAsync(updateUser);
            string errorMessage = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            if (!validationResult.IsValid)
            {
                 throw new ValidationException($"{errorMessage} is not valid");
            }

            await userService.UpdateUser(id, updateUser);
            return Ok();
        }
    }
}
