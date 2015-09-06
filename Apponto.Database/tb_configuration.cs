namespace Apponto.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_configuration
    {
        public tb_configuration()
        {
            tb_user = new HashSet<tb_user>();
        }

        [Key]
        public int id_configuration { get; set; }

        public int tb_configuration_limitation_id_configuration_limitation { get; set; }

        public virtual tb_configuration_limitation tb_configuration_limitation { get; set; }

        public virtual ICollection<tb_user> tb_user { get; set; }
    }
}
