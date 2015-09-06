using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apponto.Model
{
    public class ConfigurationLimitationModel
    {
        public ConfigurationLimitationModel()
        {
            LimitationType = new LimitationTypeModel();
        }

        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Host { get; set; }
        public LimitationTypeModel LimitationType { get; set; }
    }
}
