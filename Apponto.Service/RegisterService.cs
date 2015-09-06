using Apponto.Database;
using Apponto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apponto.Service
{
    public class RegisterService
    {
        public RegisterModel Add(RegisterModel register)
        {
            using (var appontoContext = new AppontoContext())
            {
                RegisterModel lastRegister = LoadCurrentDay(register.Date, register.User.Id, true).FirstOrDefault();

                if (lastRegister != null)
                {
                    register.IdLastRegister = lastRegister.Id;
                }
                
                tb_register tbRegister = ToDbModel(register);
                appontoContext.tb_register.Add(tbRegister);
                appontoContext.SaveChanges();

                RegisterModel ret = ToModel(tbRegister);

                return ret;
            }
        }

        public List<RegisterModel> LoadBetween(DateTime startDate, DateTime endDate, int userId, bool desc) {
            using (var appontoContext = new AppontoContext())
            {
                return Load(startDate, endDate.AddDays(1), userId, desc);
            }
        }

        public List<RegisterModel> LoadCurrentDay(DateTime date, int userId,  bool desc)
        {
            var dateFilter = DateTime.Parse(date.ToShortDateString());

            return Load(dateFilter, dateFilter.AddDays(1), userId, desc);
        }

        private List<RegisterModel> Load(DateTime startDate, DateTime endDate, int userId, bool desc)
        {
            using (var appontoContext = new AppontoContext())
            {
                List<tb_register> tbRegisterList;
                List<RegisterModel> ret = new List<RegisterModel>();
                tbRegisterList = appontoContext.tb_register.Where(x => x.tb_user.id_user == userId
                                                                 && x.dt_register >= startDate && x.dt_register < endDate).OrderBy(x => x.dt_register).ToList();
                if(desc)
                    tbRegisterList = tbRegisterList.OrderByDescending(x => x.dt_register).ToList();

                foreach (tb_register reg in tbRegisterList)
                    ret.Add(ToModel(reg));

                return ret;
            }
        }

        private tb_register ToDbModel(RegisterModel register)
        {
            tb_register tbRegister = new tb_register();

            tbRegister.id_register = register.Id;
            tbRegister.id_last_register = register.IdLastRegister;
            tbRegister.dt_register = register.Date;
            tbRegister.vl_latitude = register.Latitude;
            tbRegister.vl_longitude = register.Longitude;
            tbRegister.ds_host = register.Rede;

            if(register.User.Configuration.ConfigurationLimitation.LimitationType != null)
                tbRegister.tb_limitation_type_id_limitation_type = register.User.Configuration.ConfigurationLimitation.LimitationType.Id;

            if (register.Action != null)
                tbRegister.tb_action_id_action = register.Action.Id;

            if (register.User != null)
                tbRegister.tb_user_id_user = register.User.Id;
            
            return tbRegister;
        }
        
        private RegisterModel ToModel(tb_register tbRegister)
        {
            RegisterModel register = new RegisterModel();

            register.Id = tbRegister.id_register;
            if (tbRegister.id_last_register != null)
                register.IdLastRegister = (int)tbRegister.id_last_register;
            if (tbRegister.dt_register != null)
            {
                register.Date = (DateTime)tbRegister.dt_register;
            }
            
            if (tbRegister.vl_latitude != null)
                register.Latitude = (double)tbRegister.vl_latitude;
            if (tbRegister.vl_longitude != null)
                register.Longitude = (double)tbRegister.vl_longitude;
            register.Rede = tbRegister.ds_host;

            if(tbRegister.tb_action != null)
                register.Action = new ActionModel() { Id = tbRegister.tb_action.id_action, Name = tbRegister.tb_action.ds_action };
            
            return register;
        }
    }
}
