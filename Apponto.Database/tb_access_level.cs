namespace Apponto.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_access_level
    {
        public tb_access_level()
        {
            tb_permission = new HashSet<tb_permission>();
            tb_user = new HashSet<tb_user>();
        }

        [Key]
        public int id_access_level { get; set; }

        [StringLength(100)]
        public string ds_access_level { get; set; }

        public virtual ICollection<tb_permission> tb_permission { get; set; }

        public virtual ICollection<tb_user> tb_user { get; set; }
    }
}
