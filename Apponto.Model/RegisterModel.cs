using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apponto.Model
{
    public class RegisterModel
    {
        public int Id { get; set; }
        public int IdLastRegister { get; set; }
        public ActionModel Action { get; set; }
        public UserModel User { get; set; }
        public DateTime Date { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Rede { get; set; }

        public double DateMilliseconds
        {
            get
            {
                return Date.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            }
        }

        public double HourAgo
        {
            get
            {
                return DateTime.UtcNow.Subtract(Date).Hours;
            }
        }
    }

    public class RegisterResult
    {
        public DateTime Date { get; set; }
        List<RegisterModel> RegisterModel { get; set; }
    }
}
