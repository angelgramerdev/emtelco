using domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using domain.Entities;
using infraestructure.Contexts;
using domain.Responses;
using common;
using Microsoft.EntityFrameworkCore;

namespace infraestructure.Repositories
{
    public class PokemonRepository:IPokemonRepository<Pokemon>
    {

        
        private readonly PokemonContext _context;
        private readonly IObjResponse _objResponse;

        public PokemonRepository(PokemonContext context, IObjResponse objResponse) 
        {
            _context = context;
            _objResponse = objResponse;
        }

        public async Task<ObjResponsePokemon> GetPokemon(string name)
        {
            try 
            {
              
              var pokemon=await (from p in  _context.Pokemons where p.Name==name select p).FirstOrDefaultAsync();
              var result =await _objResponse.GetGoodResponse();
              result.Pokemon = pokemon;
            return result;  
              
            }   
            catch (Exception ex) 
            { 
                var result=await _objResponse.GetBadResponse();
               return result;  
            }
            
        }

        public async Task<List<Hability>> GetHabilities(int pokemonId) 
        {

            List<Hability> habilities = null;
            try 
            { 
                habilities=await (from h in _context.Habilities where h.PokemonId==pokemonId select h).ToListAsync();
                
                return habilities;
            }
            catch (Exception ex)
            {
                return habilities = new List<Hability>();  
            }
        }
    }
}
