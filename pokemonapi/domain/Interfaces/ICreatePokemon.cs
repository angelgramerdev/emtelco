using domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Interfaces
{
    public interface ICreatePokemon<TEntity>
    {
        Task<ObjResponsePokemon> CreatePokemon(TEntity pokemon);
    }
}
