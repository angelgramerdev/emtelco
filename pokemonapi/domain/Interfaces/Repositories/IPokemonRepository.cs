﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Interfaces.Repositories
{
    public interface IPokemonRepository<TEntity>:IGetPokemon,IGetHabilities
    {
    }
}
