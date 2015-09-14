using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apponto.Model
{
    public class UserModel
    {
        public UserModel() {
            AccessLevel = new List<AccessLevelModel>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Identification { get; set; }
        public string Password { get; set; }
        public string OldPassword { get; set; }
        public int Gmt { get; set; }
        //Deprecated
        public string Photo { get; set; }

        public CompanyModel Company { get; set; }
        public SectorModel Sector { get; set; }
        public ConfigurationModel Configuration { get; set; }
        public List<AccessLevelModel> AccessLevel { get; set; }

        public bool SavePassword { get; set; }
    }
}
