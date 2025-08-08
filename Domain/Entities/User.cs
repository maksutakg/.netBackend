using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : EntityBase
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string HashPassword { get; set; }
   
        public List<Note> Notes { get; set; }
        


    }
}
