using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.PasswordHash
{
   public interface IPasswordHasher 
    {
        string HashPassword(User user,string password);
        PasswordVerificationResult VerifyPassword(User user,string hassedPassword,string providePassword);
    }   

    }
