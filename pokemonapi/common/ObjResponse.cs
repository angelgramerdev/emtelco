using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using domain.Entities;
using domain.Responses;

namespace common
{
    public class ObjResponse:IObjResponse
    {

        public async Task<ObjResponsePokemon> GetGoodResponse() 
        {

            ObjResponsePokemon objResponsePokemon = new ObjResponsePokemon();
            objResponsePokemon.Message = "SUCCESSFUL";
            objResponsePokemon.StatusCode = HttpStatusCode.OK;
            objResponsePokemon.Code = 200;
            return await Task.FromResult(objResponsePokemon);  

        }

        public async Task<ObjResponsePokemon> GetBadResponse() 
        {
            ObjResponsePokemon objResponsePokemon = new ObjResponsePokemon();
            objResponsePokemon.Message = "Bad Request";
            objResponsePokemon.StatusCode = HttpStatusCode.BadRequest;
            objResponsePokemon.Code = 400;
            return await Task.FromResult(objResponsePokemon);
        }

    }
}
