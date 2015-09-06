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
    public class ConfigurationEmployeeController : ApiController
    {
        UserService userService = new UserService();

        [HttpGet]
        public HttpResponseMessage Get(int companyId)
        {
            try
            {
                List<UserModel> ret = userService.GetByCompany(companyId);

                return Request.CreateResponse(HttpStatusCode.OK, ret);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
