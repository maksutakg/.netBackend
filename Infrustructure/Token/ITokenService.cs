using Domain.Entities;
using Domain.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Token
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
