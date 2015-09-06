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
    [RoutePrefix("api/report")]
    public class ReportController : ApiController
    {
        RegisterService registerService = new RegisterService();

        [Route("{userId}/between")]
        [HttpGet]
        public HttpResponseMessage Get(string startDate, string endDate, int userId)
        {
            try
            {
                List<RegisterModel> ret = registerService.LoadBetween(DateTime.Parse(startDate), DateTime.Parse(endDate), userId, true);

                var results = ret.GroupBy(x => x.Date.ToShortDateString())
                                  .Select(grp => new { Key = grp.Key, List = grp.OrderBy(x => x.Date).ToList() })
                                  .ToList();
                
                return Request.CreateResponse(HttpStatusCode.OK, results);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
