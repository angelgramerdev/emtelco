using application.Interfaces;
using common;
using domain.Entities;
using domain.Interfaces.Repositories;
using domain.Responses;
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
        public AuthenticateService(IAuthenticateRepository<ObjIdentity> authenticateRepository, 
            IObjResponse objResponse) 
        { 
            _authenticateRepository = authenticateRepository;
            _objResponse = objResponse;
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

                return respose;
            }
            catch (Exception e)
            {
                return await _objResponse.GetBadResponse();
            }
        }
    }
}
