using domain.Entities;
using domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Interfaces
{
    public  interface IGetToken
    {
        Task<ObjResponsePokemon> GetToken(ObjIdentity identity);
    }
}
