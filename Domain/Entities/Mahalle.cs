using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Mahalle
    {
      public int Id { get; set; }
      public string Name { get; set; }
      public string Ilce { get; set; }="Beşiktaş";

        public List<Note> Notes { get; set; }

    }
}
