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
using System.Text;
using System.Threading.Tasks;

namespace apipokemontest
{
    
    public class AuthenticateServiceTest
    {

        [Test]
        public void CreateIdentityTest() 
        { 
            ObjIdentity identity = new ObjIdentity();
            identity.Name = "picachu";
            identity.Password = "andalu1234";
            ObjResponsePokemon objResponsePokemon = new ObjResponsePokemon();
            objResponsePokemon.StatusCode = System.Net.HttpStatusCode.OK;
            objResponsePokemon.Code = 200;
            objResponsePokemon.Message = "SUCCESSFUL";
            var task = Task.FromResult(objResponsePokemon);
            Mock<IAuthenticateService> mock=new Mock<IAuthenticateService>();
            mock.Setup(a=> a.CreateIdentity(identity)).Returns(task);
            AuthenticateService authenticateService = new AuthenticateService(It.IsAny<IAuthenticateRepository<ObjIdentity>>(), It.IsAny<IObjResponse>(),It.IsAny<ILogger<AuthenticateService>>());
            Assert.AreEqual(200, mock.Object.CreateIdentity(identity).Result.Code);
        }
    
    }
}
