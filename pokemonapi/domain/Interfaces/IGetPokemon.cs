using domain.Entities;
using domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Interfaces
{
    public interface IGetPokemon
    {
        Task<ObjResponsePokemon> GetPokemon(string name);
    
    }
}
