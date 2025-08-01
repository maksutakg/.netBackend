using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Request
{
    public class UpdateNoteRequest
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string text { get; set; }
        
    }
}
