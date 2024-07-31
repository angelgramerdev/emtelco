using application.Interfaces;
using application.Services;
using domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace pokemonapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabilityController : ControllerBase
    {
        
        private readonly IHabilityService _habilityService;
        private readonly ILogger<HabilityController> _logger;
        public HabilityController(IHabilityService habilityService, ILogger<HabilityController> logger)
        {
            _habilityService = habilityService;
            _logger = logger;
        }

        [HttpPost]
        [Authorize]
        [Route("Create_Hability")]
        public async Task<IActionResult> CreateHability([FromBody]Hability hability) 
        {
            try 
            { 
                var response=await _habilityService.CreateHability(hability);
                _logger.LogWarning("Se creó una habilidad");
                return Ok(response);
            }
            catch(Exception e) 
            {
                _logger.LogError("Se presento un error al crear la habilidad");
                return BadRequest();
            }   
        }
    
    }
}
