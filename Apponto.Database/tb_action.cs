namespace Apponto.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_action
    {
        public tb_action()
        {
            tb_register = new HashSet<tb_register>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_action { get; set; }

        [StringLength(100)]
        public string ds_action { get; set; }

        public virtual ICollection<tb_register> tb_register { get; set; }
    }
}
