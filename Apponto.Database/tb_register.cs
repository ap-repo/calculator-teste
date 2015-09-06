namespace Apponto.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_register
    {
        [Key]
        public int id_register { get; set; }

        public int? id_last_register { get; set; }

        public int tb_action_id_action { get; set; }

        public int tb_user_id_user { get; set; }

        public int tb_limitation_type_id_limitation_type { get; set; }

        public DateTime? dt_register { get; set; }

        public double? vl_latitude { get; set; }

        public double? vl_longitude { get; set; }

        [StringLength(100)]
        public string ds_host { get; set; }

        public virtual tb_action tb_action { get; set; }

        public virtual tb_limitation_type tb_limitation_type { get; set; }

        public virtual tb_user tb_user { get; set; }
    }
}
