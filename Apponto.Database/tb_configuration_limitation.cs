namespace Apponto.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_configuration_limitation
    {
        public tb_configuration_limitation()
        {
            tb_configuration = new HashSet<tb_configuration>();
        }

        [Key]
        public int id_configuration_limitation { get; set; }

        public int tb_limitation_type_id_limitation_type { get; set; }

        public double? vl_latitude { get; set; }

        public double? vl_longitude { get; set; }

        [StringLength(100)]
        public string ds_host { get; set; }

        public virtual ICollection<tb_configuration> tb_configuration { get; set; }

        public virtual tb_limitation_type tb_limitation_type { get; set; }
    }
}
