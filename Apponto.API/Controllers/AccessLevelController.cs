using Apponto.Model;
using Apponto.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Apponto.API.Controllers
{
    public class AccessLevelController : ApiController
    {
        AccessLevelService accessLevelService = new AccessLevelService();

        [HttpGet]
        public HttpResponseMessage Get()
        {
            try
            {
                List<AccessLevelModel> ret = accessLevelService.Get();

                return Request.CreateResponse(HttpStatusCode.OK, ret);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}