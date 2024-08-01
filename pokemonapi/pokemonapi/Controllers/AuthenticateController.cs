using application.Interfaces;
using common;
using domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace pokemonapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        
        private readonly IAuthenticateService _authenticateService;
        private readonly ITokenService _tokenService;
        private readonly IObjResponse _objResponse;
        private readonly ILogger<AuthenticateController> _logger;

        public AuthenticateController(IAuthenticateService authenticateService, 
            IObjResponse objResponse, 
            ITokenService tokenService, 
            ILogger<AuthenticateController> logger) 
        { 
            _authenticateService = authenticateService;
            _objResponse = objResponse; 
            _tokenService = tokenService;
            _logger = logger;   
        } 

        [HttpPost]
        [Route("register_user")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] ObjIdentity identity)
        {
            try
            {
             
                var response = await _authenticateService.CreateIdentity(identity);
                _logger.LogWarning("Se creó un nuevo usuario");
                return Ok(response);
            }
            catch (Exception e)
            {
                var response = await _objResponse.GetBadResponse();
                _logger.LogError("Fallo la creacion del usuario");
                return Ok(response);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("get_token")]
        public async Task<IActionResult> Authenticate(ObjIdentity identity) 
        {
            try 
            {               
                var result =await _tokenService.GetToken(identity);
                _logger.LogWarning("Usuario autenticado");
                return Ok(new {token=result });
            }
            catch (Exception e) 
            {
                _logger.LogWarning("Usuario o contraseña invalida");
                return BadRequest();   
            }
        }

    }
}
