using application.Interfaces;
using Castle.Core.Logging;
using domain.Entities;
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
    public class HabilityControllerTest
    {
        [Test]
        public void CreateHability() 
        {

            Hability hability = new Hability
            {
                 Name = "destruir",
                 PokemonId = 4,
                 IsHidden = true
            };
            Mock<IHabilityService> mockHabilityService = new Mock<IHabilityService>();
            Mock<ILogger<HabilityController>> mockIlogger=new Mock<ILogger<HabilityController>>();
            HabilityController habilityController = new HabilityController(mockHabilityService.Object, mockIlogger.Object);
            Assert.IsTrue(habilityController.CreateHability(hability).IsCompleted);

        }


    }
}
