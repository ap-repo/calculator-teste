using Apponto.Database;
using Apponto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apponto.Service
{
    public class UserService
    {
        public UserModel New(UserModel user)
        {
            using (var appontoContext = new AppontoContext())
            {
                #region Configurações iniciais do usuário
                ConfigurationModel configuration = new ConfigurationModel();
                ConfigurationLimitationModel configurationLimitation = new ConfigurationLimitationModel();

                ConfigurationService configurationService = new ConfigurationService();
                ConfigurationLimitationService configurationLimitationService = new ConfigurationLimitationService();

                configurationLimitation.LimitationType = new LimitationTypeModel() { Id = (int)LimitationTypeEnum.Nenhuma };
                configurationLimitation = configurationLimitationService.Add(configurationLimitation);

                configuration.ConfigurationLimitation = configurationLimitation;

                user.Configuration = configurationService.Add(configuration);
                #endregion

                tb_user tbUser = ToDbModel(user);

                #region Empresa
                bool newCompany = false;

                if (user.Company != null)
                {
                    CompanyService companyService = new CompanyService();

                    if (string.IsNullOrEmpty(user.Company.Token))
                    {
                        newCompany = true;
                        user.Company.Token = Guid.NewGuid().ToString().Substring(0, 10);
                        tbUser.tb_company = companyService.ToDbModel(user.Company);
                    }
                    else
                    {
                        tbUser.tb_company_id_company = companyService.Get(user.Company.Token).Id;
                    }
                }
                #endregion

                #region Permissões iniciais do usuário
                AccessLevelService accessLevelService = new AccessLevelService();
                List<AccessLevelModel> accessLevels = new List<AccessLevelModel>();

                if (user.Company != null && newCompany)
                    accessLevels.Add(new AccessLevelModel() { Id = (int)AccessLevelEnum.Administrator });
                else
                    accessLevels.Add(new AccessLevelModel() { Id = (int)AccessLevelEnum.User });

                user.AccessLevel = accessLevels;
                foreach (AccessLevelModel accessLevel in accessLevels)
                {
                    tb_access_level tbAccessLevel = accessLevelService.ToDbModel(accessLevel);
                    appontoContext.tb_access_level.Attach(tbAccessLevel);
                    tbUser.tb_access_level.Add(tbAccessLevel);
                }
                #endregion

                appontoContext.tb_user.Add(tbUser);
                appontoContext.SaveChanges();

                UserModel ret = ToModel(tbUser);

                return ret;
            }
        }

        public UserModel UpdatePersonalInformation(UserModel user)
        {
            using (var appontoContext = new AppontoContext())
            {
                tb_user tbUser = appontoContext.tb_user.SingleOrDefault(x => x.id_user == user.Id);

                tbUser.ds_name = user.Name;
                tbUser.ds_lastname = user.LastName;
                tbUser.id_identification = user.Identification;

                appontoContext.SaveChanges();

                UserModel ret = ToModel(tbUser);

                return ret;
            }
        }

        public bool UpdateAccessInformation(UserModel user)
        {
            using (var appontoContext = new AppontoContext())
            {
                tb_user tbUser = appontoContext.tb_user.Where(x => x.id_user == user.Id).FirstOrDefault();

                if (tbUser.ds_password != user.OldPassword)
                    return false;

                tbUser.ds_password = user.Password;

                appontoContext.SaveChanges();

                return true;
            }
        }

        public List<UserModel> Get()
        {
            using (var appontoContext = new AppontoContext())
            {
                List<tb_user> tbUsers = appontoContext.tb_user.ToList();
                List<UserModel> ret = new List<UserModel>();

                foreach (tb_user tbUser in tbUsers)
                    ret.Add(ToModel(tbUser));

                return ret;
            }
        }

        public UserModel Get(int userId)
        {
            using (var appontoContext = new AppontoContext())
            {
                tb_user tbUser = appontoContext.tb_user.Where(x => x.id_user == userId).FirstOrDefault();

                return ToModel(tbUser);

            }
        }

        public List<UserModel> GetByCompany(int companyId)
        {
            using (var appontoContext = new AppontoContext())
            {
                List<tb_user> tbUsers = appontoContext.tb_user.Where(x => x.tb_company_id_company == companyId).ToList();
                List<UserModel> ret = new List<UserModel>();

                foreach (tb_user tbUser in tbUsers)
                    ret.Add(ToModel(tbUser));

                return ret;

            }
        }

        public bool Exist(string email)
        {
            using (var appontoContext = new AppontoContext())
            {
                tb_user tbUser = appontoContext.tb_user.Where(x => x.ds_email == email).FirstOrDefault();

                return (tbUser != null);
            }
        }

        public UserModel GetByEmail(UserModel user)
        {
            using (var appontoContext = new AppontoContext())
            {
                tb_user tbUser = appontoContext.tb_user.Where(x => x.ds_email == user.Email).FirstOrDefault();

                return ToModel(tbUser);
            }
        }

        public bool Authenticate(UserModel user)
        {
            using (var appontoContext = new AppontoContext())
            {
                tb_user tbUser = appontoContext.tb_user.SingleOrDefault(x => x.ds_email == user.Email);

                return (tbUser != null && tbUser.ds_password == user.Password);
            }
        }

        private tb_user ToDbModel(UserModel user)
        {
            tb_user tbUser = new tb_user();

            tbUser.id_user = user.Id;
            tbUser.ds_email = user.Email;
            tbUser.ds_password = user.Password;
            tbUser.ds_name = user.Name;
            tbUser.ds_lastname = user.LastName;
            tbUser.id_identification = user.Identification;

            tbUser.tb_sector_id_sector = 1; //TODO: Trocar o 1 para referencia da tabela setor

            if (user.Configuration != null)
                tbUser.tb_configuration_id_configuration = user.Configuration.Id;

            foreach (AccessLevelModel accessLevel in user.AccessLevel)
                tbUser.tb_access_level.Add(new AccessLevelService().ToDbModel(accessLevel));

            return tbUser;
        }

        public UserModel ToModel(tb_user tbUser)
        {
            UserModel user = new UserModel();

            user.Id = tbUser.id_user;
            user.Email = tbUser.ds_email;
            //user.Password = tbUser.ds_password;
            user.Name = tbUser.ds_name;
            user.LastName = tbUser.ds_lastname;

            if (tbUser.tb_company != null)
                user.Company = new CompanyService().ToModel(tbUser.tb_company);

            if (tbUser.id_identification != null)
                user.Identification = (int)tbUser.id_identification;

            if (tbUser.tb_configuration != null)
                user.Configuration = new ConfigurationService().ToModel(tbUser.tb_configuration);

            foreach (tb_access_level accessLevel in tbUser.tb_access_level)
                user.AccessLevel.Add(new AccessLevelService().ToModel(accessLevel));

            return user;
        }
    }
}