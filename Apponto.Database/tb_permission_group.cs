namespace Apponto.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_permission_group
    {
        public tb_permission_group()
        {
            tb_permission = new HashSet<tb_permission>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_permission_group { get; set; }

        [StringLength(100)]
        public string ds_permission_group { get; set; }

        public virtual ICollection<tb_permission> tb_permission { get; set; }
    }
}
