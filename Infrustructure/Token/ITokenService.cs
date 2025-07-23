using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Token
{
    public interface ITokenService
    {
        string GenerateToken(string username,string role);
    }
}
