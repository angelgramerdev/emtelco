using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Entities
{
    public class Hability
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsHidden { get; set; }
        public int PokemonId { get; set; }
        public virtual Pokemon? Pokemon { get; set; }

    }
}
