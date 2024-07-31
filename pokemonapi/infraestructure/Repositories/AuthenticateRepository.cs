using common;
using domain.Entities;
using domain.Interfaces;
using domain.Interfaces.Repositories;
using domain.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace infraestructure.Repositories
{
    public class AuthenticateRepository : IAuthenticateRepository<ObjIdentity>
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IObjResponse _objResponse;
        private readonly ILogger<AuthenticateRepository> _logger;

        public AuthenticateRepository(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IObjResponse objResponse,
            ILogger<AuthenticateRepository> logger

            )
        {
            _objResponse = objResponse;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<ObjResponsePokemon> GetToken(ObjIdentity identity)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(identity.Name, identity.Password, true, true);
                ObjResponsePokemon response = null;
                if (result.Succeeded)
                {
                    response = await _objResponse.GetGoodResponse();
                    response.IsAuthenticated = true;
                    _logger.LogWarning("Se autentico el usuario");
                    return response;
                }
                else 
                { 
                    response= await _objResponse.GetBadResponse();
                    response.IsAuthenticated = false;
                    _logger.LogWarning("Usuario o contraseña invalidos");
                    return response;
                }
            }
            catch (Exception e) 
            {
                return await _objResponse.GetBadResponse();
            }
        }


        public async Task<ObjResponsePokemon> Register(ObjIdentity identity)
        {
            
            
                try
                {
                    var objIdentity = new IdentityUser
                    {
                        UserName = identity.Name
                    };

                    var result = await _userManager.CreateAsync(objIdentity, identity.Password);
                    string errors = string.Empty;
                    if (result.Errors.ToList().Count > 0)
                    {
                        foreach (var error in result.Errors.ToList())
                        {
                            errors += error.Description + ";";
                        }
                        var response = await _objResponse.GetBadResponse();
                        _logger.LogError(errors);
                        return response;
                    }
                    else
                    {
                        _logger.LogWarning("Se creo un usuario");
                        return await _objResponse.GetGoodResponse();
                    }
                }          
            catch (Exception e)
            {
                _logger.LogError("Se presento un error en la creacion del usuario");
                return await _objResponse.GetBadResponse();
            }
        }

    }
}
