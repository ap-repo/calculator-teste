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
    public class SignInController : ApiController
    {
        UserService userService = new UserService();

        //The HTTP GET method is used to retrieve (or read) a representation of a resource
        [HttpPost]
        public HttpResponseMessage Post(UserModel user)
        {
            try
            {
                if (userService.Authenticate(user))
                    return Request.CreateResponse(HttpStatusCode.Accepted, userService.GetByEmail(user));
                else
                    return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Usuário e/ou senha não conferem.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
