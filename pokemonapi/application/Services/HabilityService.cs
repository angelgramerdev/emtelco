using application.Interfaces;
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
    public class HabilityService : IHabilityService
    {

        private readonly IHabilityRepository<Hability> _habilityRepository;
        private readonly ILogger<HabilityService> _logger;
        public HabilityService(IHabilityRepository<Hability> habilityRepository, ILogger<HabilityService> logger) 
        { 
            _habilityRepository = habilityRepository;
            _logger = logger;
        }    
        
        public  async Task<ObjResponseHability> CreateHability(Hability hability)
        {
            try 
            { 
                var result=await _habilityRepository.CreateHability(hability);
                _logger.LogWarning("Se creo una habilidad");
                return result;
            }
            catch(Exception e) 
            {
                ObjResponseHability objResponseHability = new ObjResponseHability();
                objResponseHability.StatusCode = System.Net.HttpStatusCode.BadRequest;
                objResponseHability.Code = 400;
                _logger.LogError("Se presento un error al crear la habilidad");
                return await Task.FromResult(objResponseHability);
            }
        }
    }
}
