using Apponto.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apponto.Model
{
    public class PermissionService
    {
        public tb_permission ToDbModel(PermissionModel permission)
        {
            tb_permission tbPermission = new tb_permission();

            tbPermission.id_permission = permission.Id;
            tbPermission.ds_permission = permission.Name;
            tbPermission.ds_route = permission.Route;
            tbPermission.vl_order = permission.Order;
            if (permission.PermissionGroup != null)
                tbPermission.tb_permission_group_id_permission_group = permission.PermissionGroup.Id;
            
            return tbPermission;
        }

        public PermissionModel ToModel(tb_permission tbPermission)
        {
            PermissionModel permission = new PermissionModel();

            permission.Id = tbPermission.id_permission;
            permission.Name = tbPermission.ds_permission;
            permission.Route = tbPermission.ds_route;
            permission.Order = tbPermission.vl_order;
            permission.PermissionGroup = new PermissionGroupModel() { Id = tbPermission.tb_permission_group.id_permission_group };
            
            return permission;
        }
    }
}
