using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apponto.Model
{
    public class LimitationTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public enum LimitationTypeEnum
    {
        Nenhuma = 1,
        Gps = 2,
        Wifi = 3
    }
}
