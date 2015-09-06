using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apponto.Model
{
    public class ConfigurationModel
    {
        public int Id { get; set; }

        public ConfigurationLimitationModel ConfigurationLimitation { get; set; }
    }
}
