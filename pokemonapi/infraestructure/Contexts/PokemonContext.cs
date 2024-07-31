using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using domain.Entities;

namespace infraestructure.Contexts
{
    public class PokemonContext: IdentityDbContext<IdentityUser, IdentityRole, string>
    {
    
        public PokemonContext(DbContextOptions<PokemonContext> options) 
            :base(options)
        { 
        
        }
        public DbSet<Pokemon> Pokemons { get; set; } 
        public DbSet<Hability> Habilities { get; set; } 
    
    }
}
