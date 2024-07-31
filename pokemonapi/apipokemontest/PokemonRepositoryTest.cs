using common;
using domain.Entities;
using domain.Interfaces.Repositories;
using domain.Responses;
using infraestructure.Contexts;
using infraestructure.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apipokemontest
{
    public  class PokemonRepositoryTest
    {
        [Test]
        public void GetPokemon()
        {

            string name = "picachu";
            ObjResponsePokemon objResponsePokemon = new ObjResponsePokemon();
            objResponsePokemon.StatusCode = System.Net.HttpStatusCode.OK;
            objResponsePokemon.Code = 200;
            objResponsePokemon.Message = "SUCCESSFUL";
            var task = Task.FromResult(objResponsePokemon);
            Mock<IPokemonRepository<Pokemon>> mock = new Mock<IPokemonRepository<Pokemon>>();
            mock.Setup(a => a.GetPokemon(name)).Returns(task);
            PokemonRepository pokemonRepository = new PokemonRepository(It.IsAny<PokemonContext>(), It.IsAny<ObjResponse>(),It.IsAny<ILogger<PokemonRepository>>());
            var result = mock.Object.GetPokemon(name).Result;
            Assert.AreEqual(200, mock.Object.GetPokemon(name).Result.Code);
            
        
        }

        [Test]
        public void GetHabilitiesTest() 
        {
            int pokemonId = 1;
            Mock<IPokemonRepository<Pokemon>> mock = new Mock<IPokemonRepository<Pokemon>>();
            List<Hability> habilities = new List<Hability>();
            habilities.Add(new Hability
            {
                Id = 1,
                IsHidden = true,
                Name = "electrocutar",
                PokemonId = 1
            });

            habilities.Add(new Hability
            {
                Id = 2,
                IsHidden = true,
                Name = "lanzar fuego",
                PokemonId = 1
            });
            var task=Task.FromResult(habilities);   
            mock.Setup(a=> a.GetHabilities(pokemonId)).Returns(task);
            PokemonRepository pokemonRepository = new PokemonRepository(It.IsAny<PokemonContext>(), It.IsAny<ObjResponse>(),It.IsAny<ILogger<PokemonRepository>>());
            var result = mock.Object.GetHabilities(pokemonId);
            Assert.IsTrue(habilities.Count==result.Result.Count);

        }


    }
}
