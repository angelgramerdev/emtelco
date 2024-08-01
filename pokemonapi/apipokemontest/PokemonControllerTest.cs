using application.Interfaces;
using Castle.Core.Logging;
using domain.Entities;
using domain.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using pokemonapi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apipokemontest
{
    public class PokemonControllerTest
    {

        [Test]
        public void GetPockemon() 
        {
            Mock<IPokemonService> mockPockemonService = new Mock<IPokemonService>();
            PokemonController pokemonController = new PokemonController(mockPockemonService.Object, It.IsAny<ILogger<PokemonController>>());
            string name = "picachu";
            ObjResponsePokemon objResponsePokemon = new ObjResponsePokemon();
            objResponsePokemon.StatusCode = System.Net.HttpStatusCode.OK;
            objResponsePokemon.Code = 200;
            objResponsePokemon.Message = "SUCCESSFUL";
            objResponsePokemon.IsAuthenticated = false;
            var task = Task.FromResult(objResponsePokemon);
            
            mockPockemonService.Setup(a=> a.GetPokemon(name)).Returns(task);
            Assert.IsTrue(pokemonController.GetPokemon().IsCompleted);
        }

        [Test]
        public void CreatePokemon() 
        {
            Pokemon pokemon = new Pokemon
            {
                Name = "test",
                CreationDate=DateTime.Now
            };
            ObjResponsePokemon objResponsePokemon = new ObjResponsePokemon();
            objResponsePokemon.StatusCode = System.Net.HttpStatusCode.OK;
            objResponsePokemon.Code = 200;
            objResponsePokemon.Message = "SUCCESSFUL";
            objResponsePokemon.IsAuthenticated = false;
            var task=Task.FromResult(objResponsePokemon);
            Mock<IPokemonService> mockPockemonService = new Mock<IPokemonService>();
            PokemonController pokemonController = new PokemonController(mockPockemonService.Object, It.IsAny<ILogger<PokemonController>>());
            mockPockemonService.Setup(a=> a.CreatePokemon(pokemon)).Returns(task);
            Assert.IsTrue(pokemonController.CreatePokemon(pokemon).IsCompleted);
        }

    }
}
