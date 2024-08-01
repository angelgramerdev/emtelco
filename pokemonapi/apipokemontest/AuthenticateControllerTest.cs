using application.Interfaces;
using application.Services;
using Castle.Core.Logging;
using common;
using domain.Entities;
using domain.Interfaces.Repositories;
using domain.Responses;
using infraestructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
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
    public class AuthenticateControllerTest:ControllerBase
    {
        [Test]
        public async Task RegisterTest() 
        {
            
            ObjIdentity identity = new ObjIdentity();
            identity.Name = "admin";
            identity.Password = "andalu1234";
            Mock<IAuthenticateService> mockAuthenticateService = new Mock<IAuthenticateService>();
            Mock<IObjResponse> mockObjResponse= new Mock<IObjResponse>();   
            var task =await RegisterUserTest(identity);
            var result=mockAuthenticateService.Setup(a => a.CreateIdentity(identity)).ReturnsAsync(task);
            AuthenticateController authenticateController = new AuthenticateController(mockAuthenticateService.Object, mockObjResponse.Object, It.IsAny<ITokenService>(), It.IsAny<ILogger<AuthenticateController>>());
            Assert.IsTrue(authenticateController.Register(identity).IsCompleted);
            
        }

        [Test]
        public async Task Authenticate() 
        {
            ObjIdentity identity = new ObjIdentity();
            identity.Name = "admin";
            identity.Password = "andalu1234";
            Mock<ITokenService> mockTockenService = new Mock<ITokenService>();
            Mock<IAuthenticateRepository<ObjIdentity>> mockAuthenticateRepo = new Mock<IAuthenticateRepository<ObjIdentity>>();
            Mock<IObjResponse> mockObjResponse=new Mock<IObjResponse>();
            TokenService tokenService = new TokenService(It.IsAny<IConfiguration>(), mockAuthenticateRepo.Object, It.IsAny<ILogger<TokenService>>());
            Mock<IAuthenticateService> mockAuthenticateService= new Mock<IAuthenticateService>();   
            var task =await Task.FromResult(GenerateTokenTest());
            mockTockenService.Setup(a => a.GetToken(identity)).ReturnsAsync(task);
            AuthenticateController authenticateController = new AuthenticateController(mockAuthenticateService.Object, mockObjResponse.Object, mockTockenService.Object, It.IsAny<ILogger<AuthenticateController>>());
            Assert.IsTrue(authenticateController.Authenticate(identity).IsCompleted);
        }

        public async Task<ObjResponsePokemon> RegisterUserTest(ObjIdentity identity) 
        { 
            ObjResponsePokemon objResponsePokemon = new ObjResponsePokemon();
            objResponsePokemon.StatusCode=System.Net.HttpStatusCode.OK;
            objResponsePokemon.Code = 200;
            var task=await Task.FromResult(objResponsePokemon);   
           return task; 
        }

        public string GenerateTokenTest()
        {
            string token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoicHJvZ3JhbW1lciIsImV4cCI6MTcyMjQ2NzA5MH0.gZS5dGbrFCBpwj0cCk-40RWQa7yRb7CnXhZpAW9EAX3c_FSB4SApgwtL4yVcZo1y5RoEjmeGsBizQF_5ZvIU7Q";
            return token;
        }

    }
}
