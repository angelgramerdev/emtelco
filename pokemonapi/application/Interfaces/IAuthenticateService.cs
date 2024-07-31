using common;
using domain.Entities;
using domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.Interfaces
{
    public interface IAuthenticateService
    {
        Task<ObjResponsePokemon> CreateIdentity(ObjIdentity identity);
    }
}
