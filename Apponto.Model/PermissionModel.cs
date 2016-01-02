using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apponto.Model
{
    public class PermissionModel
    {
        public PermissionModel() {
            PermissionGroup = new PermissionGroupModel();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Route { get; set; }
        public int Order { get; set; }
        public string Icon { get; set; }

        public PermissionGroupModel PermissionGroup { get; set; }
    }

    public enum PermissionEnum
    { 
        Register = 1,
        Report = 2,
        Configuration = 3,
        Logout = 4,
        ConfigurationProfile = 5,
        ConfigurationEmployee = 6,
        ConfigurationPermission = 7
    }
}
