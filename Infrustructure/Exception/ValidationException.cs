using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.Exception
{
    public class ValidationException : System.Exception
    {
        public ValidationException(string message) :base(message) {}
        public ValidationException(string? message, System.Exception? innerException) : base(message, innerException) {
        
        }

      
    }
}
