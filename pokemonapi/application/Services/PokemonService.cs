using application.Interfaces;
using common;
using domain.Entities;
using domain.Interfaces.Repositories;
using domain.Responses;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.Services
{
    public class PokemonService : IPokemonService
    {
        
        private readonly IPokemonRepository<Pokemon> _pokemonRepository;
        private readonly IObjResponse _objResponse;
        private readonly ILogger<PokemonService> _logger;

        public PokemonService(IPokemonRepository<Pokemon> pokemonRepository, 
            IObjResponse objResponse,
            ILogger<PokemonService> logger
            ) 
        {
            _pokemonRepository = pokemonRepository;  
            _objResponse = objResponse;
            _logger = logger;
        }

        public async Task<ObjResponsePokemon> GetPokemon(string name)
        {
            try 
            { 
                var result=await _pokemonRepository.GetPokemon(name);
                List<Hability> habilidades =await _pokemonRepository.GetHabilities(result.Pokemon.Id);
                List<string> ocultas = new List<string>();
                if (result.Pokemon != null) 
                {
                    foreach (var h in habilidades) 
                    { 
                        if(h.IsHidden)
                            ocultas.Add(h.Name);
                    }
                }
                result.Pokemon.Habilities = null;
                result.Habilidades = new Habilidades();
                result.Habilidades.Ocultas = ocultas;
                _logger.LogWarning("se consulto un pokemon");
                return result;  
            }
            catch(Exception e) 
            { 
                var result=await _objResponse.GetBadResponse();
                _logger.LogError("Se presento un error");
                return result; 
            }   
        }
    }
}
