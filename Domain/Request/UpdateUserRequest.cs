using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Request
{
   public  class UpdateUserRequest
    {
       // public int Id { get; set; }
        public string? Name { get; set; }
        public string? SurName { get; set; }
        public string? Mail { get; set; }
        public string? Password { get; set; } 
    }
}
