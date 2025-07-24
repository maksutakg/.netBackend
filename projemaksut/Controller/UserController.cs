using Application.Service;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using Infrastructure.Exception;
using Infrastructure.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Persistence.Context;
using System.Data;
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
        private readonly ITokenService tokenService;
        private readonly AppDbContext appDbContext;
        public UserController(IUserService userService, IMapper mapper, IValidator<UserDto> validator, ITokenService tokenService, AppDbContext appDbContext)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.validator = validator;
            this.tokenService = tokenService;
            this.appDbContext = appDbContext;
        }

        [HttpPost("Auth")]
        public async Task<ActionResult<string>> Login([FromBody] LoginDto loginDto)
        {
            var user = await userService.CheckUser(loginDto.Id, loginDto.Name);
            if (user != null && user.Role=="Admin")
            {
                
                var token = tokenService.GenerateToken(user.Name, user.Role);
                return Ok(token);
            }
            return NotFound();


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
        public async Task<ActionResult<UserDto>> PostUser([FromBody] UserDto user)
        {
            var validationResult = await validator.ValidateAsync(user);
            string errorMessage = string.Join(", ", validationResult.Errors);
            if (!validationResult.IsValid) {
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
        public async Task<ActionResult<UserDto>> UpdateUser(int id, [FromBody] UserDto updateUser)
        {
            var validationResult= await  validator.ValidateAsync(updateUser);
            string errorMessage = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            if (!validationResult.IsValid)
            {
                 throw new ValidationException(  $"{errorMessage} is not valid");
            }

            await userService.UpdateUser(id, updateUser);
            return Ok();
        }

        [Authorize(Roles= "Admin")]
        [HttpGet("adminUsers")]
        public async Task<ActionResult<List<User>>> GetDetailUsers()
        {

            var users = await appDbContext.Users.ToListAsync();
            return Ok(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("adminHardDelete")]
         public async Task<ActionResult<UserDto>> HardDeleteUser(int id)
        {
            userService.DeleteUser(id);
            return Ok();

        }
    }
}
