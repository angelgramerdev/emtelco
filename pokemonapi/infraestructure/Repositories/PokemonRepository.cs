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
using Microsoft.Extensions.Logging;

namespace infraestructure.Repositories
{
    public class PokemonRepository:IPokemonRepository<Pokemon>
    {

        
        private readonly PokemonContext _context;
        private readonly IObjResponse _objResponse;
        private readonly ILogger<PokemonRepository> _logger;

        public PokemonRepository(PokemonContext context, 
            IObjResponse objResponse, 
            ILogger<PokemonRepository> logger) 
        {
            _context = context;
            _objResponse = objResponse;
            _logger = logger;
        }

        public async Task<ObjResponsePokemon> GetPokemon(string name)
        {
            try 
            {
              
              var pokemon=await (from p in  _context.Pokemons where p.Name==name select p).FirstOrDefaultAsync();
              var result =await _objResponse.GetGoodResponse();
              result.Pokemon = pokemon;
                _logger.LogWarning("Se cosulto pokemon");
            return result;  
              
            }   
            catch (Exception ex) 
            { 
                var result=await _objResponse.GetBadResponse();
                _logger.LogError("Se presento un error");
               return result;  
            }
            
        }

        public async Task<List<Hability>> GetHabilities(int pokemonId) 
        {

            List<Hability> habilities = null;
            try 
            { 
                habilities=await (from h in _context.Habilities where h.PokemonId==pokemonId select h).ToListAsync();
                _logger.LogWarning("Se consultaron las habilidades de un pokemon");

                return habilities;
            }
            catch (Exception ex)
            {
                _logger.LogError("Se presento un error");
                return habilities = new List<Hability>();  
            }
        }

        public async Task<ObjResponsePokemon> CreatePokemon(Pokemon pokemon)
        {
            try 
            {
              var result=await _context.Pokemons.AddAsync(pokemon);
              await _context.SaveChangesAsync();
              var response=await _objResponse.GetGoodResponse();
              response.Pokemon = result.Entity;
              _logger.LogWarning("Se creo un pokemon");
                return response;    
            
            }
            catch (Exception ex) 
            { 
                _logger.LogError("fallo la creacion del pokemon");  
                return await _objResponse.GetBadResponse(); 
            }
        }

    }
}
