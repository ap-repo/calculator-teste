using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apponto.Model
{
    public class MapModel
    {
        public int Zoom { get; set; }

        public CenterModel Center;
    }

    public class CenterModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}