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
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        UserService userService = new UserService();
        ConfigurationLimitationService limitationService = new ConfigurationLimitationService();
        AccessLevelService accessLevelService = new AccessLevelService();

        [HttpGet]
        public HttpResponseMessage Get(int userId)
        {
            UserModel ret = userService.Get(userId);

            return Request.CreateResponse(HttpStatusCode.OK, ret);
        }

        [Route("{email}/exist")]
        [HttpGet]
        public HttpResponseMessage Exist(string email)
        {
            bool ret = userService.Exist(email);

            return Request.CreateResponse(HttpStatusCode.OK, ret);
        }

        [Route("{userId}/accesslevel")]
        [HttpGet]
        public HttpResponseMessage AccessLevel(int userId)
        {
            List<AccessLevelModel> ret = accessLevelService.Get(userId);

            return Request.CreateResponse(HttpStatusCode.OK, ret);
        }

        [Route("{userId}/accesslevel")]
        [HttpPut]
        public HttpResponseMessage AccessLevelPut(int userId, List<AccessLevelModel> accessLevels)
        {
            accessLevelService.Update(userId, accessLevels);

            return Request.CreateResponse(HttpStatusCode.OK, "Permissão salva com sucesso.");
        }

        [Route("{userId}/limitation")]
        [HttpPut]
        public HttpResponseMessage Put(int userId, ConfigurationLimitationModel limitation)
        {
            ConfigurationLimitationModel limitationModel = new ConfigurationLimitationModel();

            if (limitation.LimitationType.Id == (int)LimitationTypeEnum.Gps)
            {
                limitationModel.LimitationType.Id = (int)LimitationTypeEnum.Gps;
                limitationModel.Latitude = limitation.Latitude;
                limitationModel.Longitude = limitation.Longitude;
            }
            else if (limitation.LimitationType.Id == (int)LimitationTypeEnum.Wifi)
            {
                limitationModel.LimitationType.Id = (int)LimitationTypeEnum.Wifi;
                limitationModel.Host = limitation.Host;
            }
            else
                limitation.LimitationType.Id = (int)LimitationTypeEnum.Nenhuma;

            limitationService.Update(userId, limitation);

            return Request.CreateResponse(HttpStatusCode.OK, "Restrição salva com sucesso.");
        }
    }
}
