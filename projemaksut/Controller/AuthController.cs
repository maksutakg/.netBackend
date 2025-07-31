using Application.Service;
using Domain.Entities;
using Domain.Request;
using Infrastructure.Exception;
using Infrastructure.PasswordHash;
using Infrastructure.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace projemaksut.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService tokenService;
        private readonly AppDbContext context;
        private readonly IPasswordHasher passwordHasher;
        
        public AuthController(ITokenService tokenService, IPasswordHasher passwordHasher, AppDbContext context)
        {
            this.tokenService = tokenService;
            this.passwordHasher = passwordHasher;
            this.context = context;
        }
       

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(LoginRequest login)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Mail == login.Mail);
            if (user == null) { throw new NotFoundException("hatalı mail"); }
            var result=passwordHasher.VerifyPassword(user, user.HashPassword, login.Password);
            if (result == false) { throw new NotFoundException("hatalı şifre"); }
            return  tokenService.GenerateToken(user);
                
        }
    }
}
