using common;
using domain.Entities;
using domain.Interfaces.Repositories;
using domain.Responses;
using infraestructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apipokemontest
{
    public class AuthenticateRepositoryTest
    {

        [Test]
        public void Register() 
        {

            ObjIdentity identity = new ObjIdentity();
            identity.Name = "admin";
            identity.Password = "andalu1234";
            ObjResponsePokemon objResponsePokemon = new ObjResponsePokemon();
            objResponsePokemon.StatusCode=System.Net.HttpStatusCode.OK;
            objResponsePokemon.Code = 200;
            objResponsePokemon.Message = "SUCCESSFUL";
            var task=Task.FromResult(objResponsePokemon);   
            Mock<IAuthenticateRepository<ObjIdentity>> mock = new Mock<IAuthenticateRepository<ObjIdentity>>();
            Mock<IObjResponse> objResponse = new Mock<IObjResponse>();
            Mock<ILogger<AuthenticateRepository>> logger=new Mock<ILogger<AuthenticateRepository>>();
            AuthenticateRepository authenticateRepository = new AuthenticateRepository(It.IsAny<UserManager<IdentityUser>>(), It.IsAny<SignInManager<IdentityUser>>(), objResponse.Object, logger.Object);
            mock.Setup(a => a.Register(identity)).Returns(task);
            Assert.AreEqual(200, mock.Object.Register(identity).Result.Code);

        }

        [Test]
        public void GetTokenTrue() 
        {
            ObjIdentity identity = new ObjIdentity();
            identity.Name = "admin";
            identity.Password = "andalu1234";
            ObjResponsePokemon objResponsePokemon = new ObjResponsePokemon();
            objResponsePokemon.StatusCode = System.Net.HttpStatusCode.OK;
            objResponsePokemon.Code = 200;
            objResponsePokemon.Message = "SUCCESSFUL";
            objResponsePokemon.IsAuthenticated = true;  
            var task = Task.FromResult(objResponsePokemon);
            Mock<IAuthenticateRepository<ObjIdentity>> mock = new Mock<IAuthenticateRepository<ObjIdentity>>();
            mock.Setup(a => a.Register(identity)).Returns(task);
            Assert.AreEqual(true, mock.Object.Register(identity).Result.IsAuthenticated);

        }

        [Test]
        public void GetTokenFalse()
        {
            ObjIdentity identity = new ObjIdentity();
            identity.Name = "admin1";
            identity.Password = "andalu1234";
            ObjResponsePokemon objResponsePokemon = new ObjResponsePokemon();
            objResponsePokemon.StatusCode = System.Net.HttpStatusCode.OK;
            objResponsePokemon.Code = 200;
            objResponsePokemon.Message = "SUCCESSFUL";
            objResponsePokemon.IsAuthenticated =false;
            var task = Task.FromResult(objResponsePokemon);
            Mock<IAuthenticateRepository<ObjIdentity>> mock = new Mock<IAuthenticateRepository<ObjIdentity>>();
            mock.Setup(a => a.Register(identity)).Returns(task);
            Assert.AreEqual(false, mock.Object.Register(identity).Result.IsAuthenticated);

        }


    }
}
