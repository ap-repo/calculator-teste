namespace Apponto.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_user
    {
        public tb_user()
        {
            tb_register = new HashSet<tb_register>();
            tb_access_level = new HashSet<tb_access_level>();
        }

        [Key]
        public int id_user { get; set; }

        public int tb_configuration_id_configuration { get; set; }

        public int tb_sector_id_sector { get; set; }

        public int? id_identification { get; set; }

        [StringLength(100)]
        public string ds_email { get; set; }

        [StringLength(100)]
        public string ds_password { get; set; }

        [StringLength(100)]
        public string ds_name { get; set; }

        [StringLength(100)]
        public string ds_lastname { get; set; }

        public int? id_user_superior { get; set; }

        [StringLength(100)]
        public string ds_photo { get; set; }

        public int? tb_company_id_company { get; set; }

        public int? vl_gmt { get; set; }

        public virtual tb_company tb_company { get; set; }

        public virtual tb_configuration tb_configuration { get; set; }

        public virtual ICollection<tb_register> tb_register { get; set; }

        public virtual tb_sector tb_sector { get; set; }

        public virtual ICollection<tb_access_level> tb_access_level { get; set; }
    }
}
