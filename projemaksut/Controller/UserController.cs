using Application.Service;
using AutoMapper;
using Domain.Entities;
using Domain.Request;
using FluentValidation;
using Infrastructure.Exception;
using Infrastructure.Token;
using Infrastructure.Validators;
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
        private readonly ITokenService tokenService;
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly AppDbContext appDbContext;
        private readonly IValidator<User> validator;
        public UserController(IUserService userService, IMapper mapper, ITokenService tokenService, AppDbContext appDbContext, IValidator<User> validator)
        {
            this.userService = userService;
            this.mapper = mapper;
            this.tokenService = tokenService;
            this.appDbContext = appDbContext;
            this.validator = validator;
        }

        [HttpPost("Auth")]
        public async Task<ActionResult<string>> Login(int id, string mail, string role)
        {
            var checkUser = await userService.CheckUser(id, mail, role);
            if (checkUser == true)
            {
                var token = tokenService.GenerateToken(id,mail,role);
                return Ok(token);
            }
              throw new NotFoundException("ID and name do not match");


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
        public async Task<ActionResult<UserDto>> PostUser([FromBody] CreateUserRequest createUser)
        {
            var user = mapper.Map<User>(createUser);
            var validationResult = await validator.ValidateAsync(user);
            string errorMessage = string.Join(", ", validationResult.Errors);
            if (validationResult.IsValid) {
               
                return await userService.CreateUser(createUser);
            }
            throw new ValidationException(errorMessage);

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
        public async Task<ActionResult<UserDto>> UpdateUser([FromBody] UpdateUserRequest updateUser)
        {
            var user = mapper.Map<User>(updateUser);
            var validationResult =await validator.ValidateAsync(user);
            string errorMessage = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            if (!validationResult.IsValid)
            {
                 throw new ValidationException(  $"{errorMessage}");
            }

            await userService.UpdateUser(updateUser);
            return Ok();
        }

        [Authorize(Roles= "Admin")]
        [HttpGet("adminUsers")]
        public async Task<ActionResult<List<User>>> GetDetailUsers()
        {
            return await userService.UserDetail();
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("adminHardDelete/{id}")]
         public async Task<ActionResult<UserDto>> HardDeleteUser(int id)
        {
            var user = await userService.GetUser(id);
            if (user == null) { throw new NotFoundException("bulunamadı"); }
            await  userService.HardDelete(id);
            return Ok();

        }


    }
}
