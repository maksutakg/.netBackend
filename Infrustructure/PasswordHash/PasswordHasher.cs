     using Domain.Entities;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    namespace Infrastructure.PasswordHash
    {
        public class PasswordHasher : IPasswordHasher
        {
            private readonly PasswordHasher<User> _hasher;

            public PasswordHasher(PasswordHasher<User> hasher)
            {
                _hasher = hasher;
            }

            public string HashPassword(User user,string password)
            {
                return _hasher.HashPassword(user , password);
            }

            public PasswordVerificationResult VerifyPassword( User user,string hashPassword, string providePassword)
            {
               var result = _hasher.VerifyHashedPassword(user,hashPassword, providePassword);
            return result;
            }
        }
    }
