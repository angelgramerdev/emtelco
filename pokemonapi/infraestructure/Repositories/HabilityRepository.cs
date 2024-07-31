using domain.Entities;
using domain.Interfaces.Repositories;
using domain.Responses;
using infraestructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infraestructure.Repositories
{
    public class HabilityRepository:IHabilityRepository<Hability>
    {

       private readonly PokemonContext _context; 
       private readonly ILogger<HabilityRepository> _logger;
        public HabilityRepository(PokemonContext context, ILogger<HabilityRepository> logger) 
        {
            _context = context;
            _logger = logger;
        } 
        
        public async Task<ObjResponseHability> CreateHability(Hability hability)
        {
            ObjResponseHability responseHability = null;


            try
            {
                var result = await _context.Habilities.AddAsync(hability);
                await _context.SaveChangesAsync();
                responseHability = new ObjResponseHability();

                if (result.Entity.Id > 0)
                {
                    responseHability.StatusCode = System.Net.HttpStatusCode.OK;
                    responseHability.Code = 200;
                    responseHability.hability = result.Entity;
                    _logger.LogWarning("Se creao una habilidad");
                }
                _logger.LogWarning("Se creo una habilidad");
                return responseHability;    
            }
            catch (Exception ex)
            {
                responseHability=new ObjResponseHability();
                responseHability.Code = 400;
                responseHability.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _logger.LogError("Se presentó un error creando la habilidad");
                return responseHability;
            }
        }

    }
}
