using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserDto
    {
        public string name { get; set; }
        public string surName { get; set; }
        public string Note { get; set; }
        public string Mail { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        
    }
}
