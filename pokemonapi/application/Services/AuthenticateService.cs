using application.Interfaces;
using common;
using domain.Entities;
using domain.Interfaces.Repositories;
using domain.Responses;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IAuthenticateRepository<ObjIdentity> _authenticateRepository;
        private readonly IObjResponse _objResponse;
        private readonly ILogger<AuthenticateService> _logger;
        public AuthenticateService(IAuthenticateRepository<ObjIdentity> authenticateRepository, 
            IObjResponse objResponse,
            ILogger<AuthenticateService> logger
            ) 
        { 
            _authenticateRepository = authenticateRepository;
            _objResponse = objResponse;
            _logger = logger;
        }

        public async Task<ObjResponsePokemon> CreateIdentity(ObjIdentity identity)
        {
            try
            {
                if (identity == null)
                {
                    return await _objResponse.GetBadResponse();
                }
                var respose = await _authenticateRepository.Register(identity);
                _logger.LogWarning("Se creo un usuario");
                return respose;
            }
            catch (Exception e)
            {
                _logger.LogError("Se presento un error");
                return await _objResponse.GetBadResponse();
            }
        }
    }
}
