using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Exception
{
    public class NotFoundException : System.Exception
    {
        public NotFoundException(string? message):base(message) {}
        public NotFoundException(string? message, System.Exception? innerException) : base(message, innerException){}
    }
}
