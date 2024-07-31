using domain.Entities;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace domain.Responses
{
    public class ObjResponsePokemon
    {
        public string Message { get; set; }
        public int Code { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public Pokemon Pokemon { get; set; }
        public bool IsAuthenticated { get; set; }
        public Habilidades Habilidades { get; set; }

    }
}
