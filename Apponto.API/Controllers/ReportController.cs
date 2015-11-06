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
                                  .Select(grp => new { Key = grp.Key, List = grp.OrderBy(x => x.Date).ToList(), Worked = totalRegisterWorked(grp.OrderBy(x => x.Date).ToList()) })
                                  .ToList();
                
                return Request.CreateResponse(HttpStatusCode.OK, results);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        private double totalRegisterWorked(List<RegisterModel> ret) {
            double total = 0;
            for (int i = 0; i < ret.Count - 1; i++)
            {
                if (ret[i].Action.Id == (int)ActionEnum.Start)
                {
                    if (ret[i + 1].Action.Id == (int)ActionEnum.Break)
                    {
                        total += ret[i + 1].Date.Subtract(ret[i].Date).TotalMilliseconds;
                    }
                    else if (ret[i + 1].Action.Id == (int)ActionEnum.End)
                    {
                        total += ret[i + 1].Date.Subtract(ret[i].Date).TotalMilliseconds;
                        break;
                    }
                    else
                    {
                        //TODO: substrair pelo horário atual
                    }
                }
                else if (ret[i].Action.Id == (int)ActionEnum.BreakBack)
                {
                    if (ret[i + 1].Action.Id == (int)ActionEnum.Break)
                    {
                        total += ret[i + 1].Date.Subtract(ret[i].Date).TotalMilliseconds;
                    }
                    else if (ret[i + 1].Action.Id == (int)ActionEnum.End)
                    {
                        total += ret[i + 1].Date.Subtract(ret[i].Date).TotalMilliseconds;
                        break;
                    }
                    else
                    {

                    }
                }
            }

            //total = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(total).Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

            return total;
        }
    }
}
