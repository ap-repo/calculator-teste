namespace Apponto.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_sector
    {
        public tb_sector()
        {
            tb_user = new HashSet<tb_user>();
        }

        [Key]
        public int id_sector { get; set; }

        [StringLength(100)]
        public string ds_sector { get; set; }

        public virtual ICollection<tb_user> tb_user { get; set; }
    }
}
