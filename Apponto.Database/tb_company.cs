namespace Apponto.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_company
    {
        public tb_company()
        {
            tb_user = new HashSet<tb_user>();
        }

        [Key]
        public int id_company { get; set; }

        [Required]
        [StringLength(10)]
        public string ds_token { get; set; }

        [StringLength(100)]
        public string ds_name { get; set; }

        public virtual ICollection<tb_user> tb_user { get; set; }
    }
}
