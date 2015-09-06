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
    public class SignUpController : ApiController
    {
        UserService userService = new UserService();

        //The POST verb is most-often utilized for creation of new resources
        [HttpPost]
        public HttpResponseMessage Post(UserModel user)
        {
            try
            {
                if(userService.Exist(user.Email))
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Email já registrado.");

                UserModel ret = userService.GetByEmail(userService.New(user));

                new EmailService().SendWelcomeEmail(ret);

                return Request.CreateResponse(HttpStatusCode.Created, ret);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
