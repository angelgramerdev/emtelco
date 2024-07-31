using domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace domain.Responses
{
    public class ObjResponseHability
    {
        public string Message { get; set; }
        public int Code { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public Hability hability { get; set; }
    }
}
