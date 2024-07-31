using application.Interfaces;
using domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace pokemonapi.Controllers
{
    
    [ApiController]
    public class PokemonController : ControllerBase
    {

        private readonly IPokemonService _pokemonService;
        private readonly ILogger<PokemonController> _logger;   

        public PokemonController(IPokemonService pokemonService, ILogger<PokemonController> logger) 
        { 
            _pokemonService = pokemonService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        [Route("pokemon/habilidadesOcultas/{pokemon}")]
        public async Task<IActionResult> GetPokemon() 
        {
            try 
            {
                string name = HttpContext.Request.RouteValues["pokemon"].ToString();
                var result=await _pokemonService.GetPokemon(name);
                _logger.LogWarning("metodo GetPokemon ejecutado");
                return Ok(result);  
            }
            catch (Exception ex) 
            {
                _logger.LogError("Se presento un error o no se encontro informacion");
                return BadRequest();  
            }
        
        }

        [HttpPost]
        [Authorize]
        [Route("Create_Pokemon")]
        public async Task<IActionResult> CreatePokemon(Pokemon pokemon) 
        {
            try 
            { 
             var result=await _pokemonService.CreatePokemon(pokemon);
             _logger.LogWarning("Se creo un pokemon");  
             return Ok(result);    
            
            }
            catch (Exception ex) 
            { 
                _logger.LogError("Fallo la creacion del pokemon");
                return BadRequest();
            }
        }
    
    }
}
