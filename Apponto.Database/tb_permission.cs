namespace Apponto.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_permission
    {
        public tb_permission()
        {
            tb_access_level = new HashSet<tb_access_level>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_permission { get; set; }

        public int tb_permission_group_id_permission_group { get; set; }

        [StringLength(100)]
        public string ds_permission { get; set; }

        [StringLength(50)]
        public string ds_route { get; set; }

        public int vl_order { get; set; }

        public virtual tb_permission_group tb_permission_group { get; set; }

        public virtual ICollection<tb_access_level> tb_access_level { get; set; }
    }
}
