using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authdeneme
{
    public class JwtSettings
    {
        // kim tarafından verildi audience 
         public string Audience { get; set; }
        // kim tarafından yayınlanıcak 
        public string İssuer {  get; set; }

        //secret key 
        public string Key { get; set; }
        public int Expire { get; set; }"
        
    }
}
