using Apponto.Database;
using Apponto.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
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

                register.Gmt = register.User.Gmt;

                tb_register tbRegister = ToDbModel(register);
                appontoContext.tb_register.Add(tbRegister);
                appontoContext.SaveChanges();

                RegisterModel ret = ToModel(tbRegister);

                return ret;
            }
        }

        /// <summary>
        /// Busca registros de ponto de uma determinado período e usuário
        /// </summary>
        /// <param name="startDate">Data em formato GMT</param>
        /// <param name="endDate">Data em formato GMT</param>
        /// <param name="userId">Id do usuário</param>
        /// <param name="desc">Verdadeiro para ordernar decrescente e Falso para ordernar crescente</param>
        /// <returns></returns>
        public List<RegisterModel> LoadBetween(DateTime startDate, DateTime endDate, int userId, bool desc) {
            using (var appontoContext = new AppontoContext())
            {
                return Load(startDate, endDate.AddDays(1), userId, desc);
            }
        }

        /// <summary>
        /// Busca os registros de ponto de um dia atual para um determinado usuário
        /// </summary>
        /// <param name="date">Data em formato UTC</param>
        /// <param name="userId">Id do usuário</param>
        /// <param name="desc">Verdadeiro para ordernar decrescente e Falso para ordernar crescente</param>
        /// <returns></returns>
        public List<RegisterModel> LoadCurrentDay(DateTime date, int userId,  bool desc)
        {
            var dateFilter = DateTime.Parse(date.AddHours(-3).ToShortDateString());

            return Load(dateFilter, dateFilter.AddDays(1), userId, desc);
        }

        private List<RegisterModel> Load(DateTime startDate, DateTime endDate, int userId, bool desc)
        {
            using (var appontoContext = new AppontoContext())
            {
                List<tb_register> tbRegisterList;
                List<RegisterModel> ret = new List<RegisterModel>();
                tbRegisterList = appontoContext.tb_register.Where(x => x.tb_user.id_user == userId
                                                                 && DbFunctions.AddHours(x.dt_register, x.vl_gmt) >= startDate && DbFunctions.AddHours(x.dt_register, x.vl_gmt) < endDate).OrderBy(x => x.dt_register).ToList();
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
            tbRegister.ds_host = register.Host;
            tbRegister.vl_gmt = register.Gmt;

            if (register.User.Configuration.ConfigurationLimitation.LimitationType != null)
            {
                tbRegister.tb_limitation_type_id_limitation_type = register.User.Configuration.ConfigurationLimitation.LimitationType.Id;
                tbRegister.id_limitation_type = register.User.Configuration.ConfigurationLimitation.LimitationType.Id;
            }

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
                register.Date = (DateTime)tbRegister.dt_register.Value.AddHours(Double.Parse(tbRegister.vl_gmt.ToString()));
            }
            
            if (tbRegister.vl_latitude != null)
                register.Latitude = (double)tbRegister.vl_latitude;
            if (tbRegister.vl_longitude != null)
                register.Longitude = (double)tbRegister.vl_longitude;
            register.Host = tbRegister.ds_host;

            if (tbRegister.id_limitation_type != null)
                register.IdLimitationType = (int)tbRegister.id_limitation_type;

            if(tbRegister.tb_action != null)
                register.Action = new ActionModel() { Id = tbRegister.tb_action.id_action, Name = tbRegister.tb_action.ds_action };

            if (tbRegister.vl_gmt != null)
                register.Gmt = (int)tbRegister.vl_gmt;

            if (tbRegister.tb_user != null)
                register.User = new UserModel() { Id = tbRegister.tb_user.id_user, Name = tbRegister.tb_user.ds_name, LastName = tbRegister.tb_user.ds_lastname, Email = tbRegister.tb_user.ds_email };

            return register;
        }
    }
}
