using domain.Entities;
using domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Interfaces
{
    public interface IRegisterUser
    {
        Task<ObjResponsePokemon> Register(ObjIdentity identity);
    }
}
