using application.Interfaces;
using application.Services;
using Castle.Core.Logging;
using domain.Entities;
using domain.Interfaces.Repositories;
using infraestructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apipokemontest
{
    public class TokenServiceTest
    {
        [Test]
        public void GetTokenTest() 
        { 
            
            ObjIdentity identity = new ObjIdentity();
            identity.Name = "admin";
            identity.Password = "andalu1234";
            
            Mock<ITokenService> mock= new Mock<ITokenService>();
            TokenService tokenService = new TokenService(It.IsAny<IConfiguration>(),
                It.IsAny<IAuthenticateRepository<ObjIdentity>>(), It.IsAny<ILogger<TokenService>>());

            mock.Setup(a => a.GetToken(identity)).Returns(Task.FromResult(GenerateTokenTest()));
            Assert.IsTrue(mock.Object.GetToken(identity).Result.Length > 0);
        }

        public string GenerateTokenTest() 
        {
            string token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoicHJvZ3JhbW1lciIsImV4cCI6MTcyMjQ2NzA5MH0.gZS5dGbrFCBpwj0cCk-40RWQa7yRb7CnXhZpAW9EAX3c_FSB4SApgwtL4yVcZo1y5RoEjmeGsBizQF_5ZvIU7Q";
            return token;
        }
    
    }
}
