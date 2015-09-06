namespace Apponto.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_limitation_type
    {
        public tb_limitation_type()
        {
            tb_configuration_limitation = new HashSet<tb_configuration_limitation>();
            tb_register = new HashSet<tb_register>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_limitation_type { get; set; }

        [StringLength(100)]
        public string ds_limitation_type { get; set; }

        public virtual ICollection<tb_configuration_limitation> tb_configuration_limitation { get; set; }

        public virtual ICollection<tb_register> tb_register { get; set; }
    }
}
