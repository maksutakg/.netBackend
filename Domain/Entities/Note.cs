using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public string text { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        
        
        public int UserId { get; set; }
        public User user { get; set; } = null!;
    }
}
