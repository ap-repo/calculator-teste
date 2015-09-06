using Apponto.Database;
using Apponto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apponto.Service
{
    public class AccessLevelService
    {
        public List<AccessLevelModel> Get()
        {
            using (var appontoContext = new AppontoContext())
            {
                List<tb_access_level> tbAccessLevels = appontoContext.tb_access_level.ToList();
                List<AccessLevelModel> ret = new List<AccessLevelModel>();

                foreach (tb_access_level tbAccessLevel in tbAccessLevels)
                    ret.Add(ToModel(tbAccessLevel));

                return ret;
            }
        }

        public List<AccessLevelModel> Get(int userId)
        {
            using (var appontoContext = new AppontoContext())
            {
                tb_user tbUser = appontoContext.tb_user.Where(x => x.id_user == userId).FirstOrDefault();
                List<AccessLevelModel> ret = new List<AccessLevelModel>();

                foreach (tb_access_level accessLevel in tbUser.tb_access_level)
                    ret.Add(ToModel(accessLevel));

                return ret;
            }
        }

        public bool Update(int userId, List<AccessLevelModel> accessLevels)
        {
            if (Remove(userId))
                return Add(userId, accessLevels);
            else
                return false;
        }

        private bool Add(int userId, List<AccessLevelModel> accessLevels)
        {
            using (var appontoContext = new AppontoContext())
            {
                tb_user tbUser = appontoContext.tb_user.Where(x => x.id_user == userId).FirstOrDefault();

                foreach (AccessLevelModel accessLevel in accessLevels)
                {
                    tb_access_level tbAccessLevel = ToDbModel(accessLevel);

                    appontoContext.tb_access_level.Attach(tbAccessLevel);
                    tbAccessLevel.tb_user.Add(tbUser);
                }

                appontoContext.SaveChanges();

                return true;
            }
        }

        private bool Remove(int userId)
        {
            using (var appontoContext = new AppontoContext())
            {
                tb_user tbUser = appontoContext.tb_user.Where(x => x.id_user == userId).FirstOrDefault();

                var accessLevels = tbUser.tb_access_level.ToList();
                accessLevels.ForEach(x => tbUser.tb_access_level.Remove(x));

                appontoContext.SaveChanges();

                return true;
            }
        }

        public tb_access_level ToDbModel(AccessLevelModel accessLevel)
        {
            tb_access_level tbAccessLevel = new tb_access_level();

            tbAccessLevel.id_access_level = accessLevel.Id;
            tbAccessLevel.ds_access_level = accessLevel.Name;

            return tbAccessLevel;
        }

        public AccessLevelModel ToModel(tb_access_level tbAccessLevel)
        {
            AccessLevelModel accessLevel = new AccessLevelModel();

            accessLevel.Id = tbAccessLevel.id_access_level;
            accessLevel.Name = tbAccessLevel.ds_access_level;

            foreach (tb_permission permission in tbAccessLevel.tb_permission)
                accessLevel.Permissions.Add(new PermissionService().ToModel(permission));



            return accessLevel;
        }
    }
}
