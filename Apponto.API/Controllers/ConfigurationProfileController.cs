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
    [RoutePrefix("api/configurationProfile")]
    public class ConfigurationProfileController : ApiController
    {
        UserService userService = new UserService();

        [Route("personal")]
        [HttpPut]
        public HttpResponseMessage PutPersonal(UserModel user)
        {
            userService.UpdatePersonalInformation(user);

            return Request.CreateResponse(HttpStatusCode.OK, "Informações pessoais salvas com sucesso.");
        }

        [Route("access")]
        [HttpPut]
        public HttpResponseMessage PutAccess(UserModel user)
        {
            if(userService.UpdateAccessInformation(user))
                return Request.CreateResponse(HttpStatusCode.OK, "Informações de acesso salvas com sucesso.");
            else
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Senha atual não confere.");
        }
    }
}
