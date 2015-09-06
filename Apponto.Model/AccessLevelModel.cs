using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apponto.Model
{
    public class AccessLevelModel
    {
        public AccessLevelModel()
        {
            Permissions = new List<PermissionModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public List<PermissionModel> Permissions { get; set; }
    }
    
    public enum AccessLevelEnum
    {
        Administrator = 1,
        User = 2
    }
}
