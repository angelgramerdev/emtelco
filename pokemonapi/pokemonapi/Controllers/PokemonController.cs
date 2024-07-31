using application.Interfaces;
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
                _logger.LogWarning("metodo GetPokemon en ejecucion");
                string name = HttpContext.Request.RouteValues["pokemon"].ToString();
                var result=await _pokemonService.GetPokemon(name);
                return Ok(result);  
            }
            catch (Exception ex) 
            { 
                return BadRequest();  
            }
        
        }
    
    }
}
