using application.Interfaces;
using application.Services;
using common;
using domain.Entities;
using domain.Interfaces.Repositories;
using domain.Responses;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace apipokemontest
{
    public class PokemonServiceTest
    {
        [Test]
        public void GetPokemonTest() 
        {
            string name = "picachu";
            ObjResponsePokemon objResponsePokemon = new ObjResponsePokemon();
            objResponsePokemon.StatusCode = System.Net.HttpStatusCode.OK;
            objResponsePokemon.Code = 200;
            objResponsePokemon.Message = "SUCCESSFUL";
            var task = Task.FromResult(objResponsePokemon);
            Mock<IPokemonService> mock = new Mock<IPokemonService>();
            mock.Setup(a=> a.GetPokemon(name)).Returns(task);
            PokemonService pokemonService = new PokemonService(It.IsAny<IPokemonRepository<Pokemon>>(),It.IsAny<IObjResponse>(),It.IsAny<ILogger<PokemonService>>());
            Assert.AreEqual(200, mock.Object.GetPokemon(name).Result.Code);

        }
    
    }
}
