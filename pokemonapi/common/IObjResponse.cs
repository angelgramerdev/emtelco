using domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    public interface IObjResponse
    {
        Task<ObjResponsePokemon> GetGoodResponse();
        Task<ObjResponsePokemon> GetBadResponse();
    }
}
