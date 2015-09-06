using Apponto.Model;
using Apponto.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Apponto.API.Controllers
{
    [RoutePrefix("api/register")]
    public class RegisterController : ApiController
    {
        RegisterService registerService = new RegisterService();

        [HttpPost]
        public HttpResponseMessage Post(RegisterModel register)
        {
            register.Date = DateTime.UtcNow;

            registerService.Add(register);

            return Request.CreateResponse(HttpStatusCode.OK, "Ponto registrado com sucesso.");
        }

        [Route("{userId}/day")]
        [HttpGet]
        public HttpResponseMessage Get(int userId)
        {
            List<RegisterModel> ret = registerService.LoadCurrentDay(DateTime.UtcNow, userId, false);

            return Request.CreateResponse(HttpStatusCode.OK, ret);
        }
    }
}
