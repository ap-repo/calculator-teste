using Apponto.Database;
using Apponto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apponto.Service
{
    public class ConfigurationLimitationService
    {
        public ConfigurationLimitationModel Add(ConfigurationLimitationModel configurationLimitation)
        {
            using (var appontoContext = new AppontoContext())
            {
                tb_configuration_limitation tbConfiguracaoLimitacao= ToDbModel(configurationLimitation);
                appontoContext.tb_configuration_limitation.Add(tbConfiguracaoLimitacao);
                appontoContext.SaveChanges();

                ConfigurationLimitationModel ret = ToModel(tbConfiguracaoLimitacao);
                
                return ret;
            }
        }

        public UserModel Update(int userId, ConfigurationLimitationModel configurationLimitation)
        {
            using (var appontoContext = new AppontoContext())
            {
                tb_user tbUser = appontoContext.tb_user.SingleOrDefault(x => x.id_user == userId);

                tbUser.tb_configuration.tb_configuration_limitation.tb_limitation_type_id_limitation_type = configurationLimitation.LimitationType.Id;
                if (configurationLimitation.LimitationType.Id == (int)LimitationTypeEnum.Gps)
                {
                    tbUser.tb_configuration.tb_configuration_limitation.vl_latitude = configurationLimitation.Latitude;
                    tbUser.tb_configuration.tb_configuration_limitation.vl_longitude = configurationLimitation.Longitude;
                }
                else if (configurationLimitation.LimitationType.Id == (int)LimitationTypeEnum.Wifi)
                {
                    tbUser.tb_configuration.tb_configuration_limitation.ds_host = configurationLimitation.Host;
                }
                
                appontoContext.SaveChanges();

                UserModel ret = new UserService().ToModel(tbUser);

                return ret;
            }
        }

        private tb_configuration_limitation ToDbModel(ConfigurationLimitationModel configurationLimitation)
        {
            tb_configuration_limitation tbConfigurationLimitation = new tb_configuration_limitation();

            tbConfigurationLimitation.vl_latitude = configurationLimitation.Latitude;
            tbConfigurationLimitation.vl_longitude = configurationLimitation.Longitude;
            tbConfigurationLimitation.ds_host = configurationLimitation.Host;

            if(configurationLimitation.LimitationType != null)
                tbConfigurationLimitation.tb_limitation_type_id_limitation_type = configurationLimitation.LimitationType.Id;

            return tbConfigurationLimitation;
        }

        public ConfigurationLimitationModel ToModel(tb_configuration_limitation tbConfigurationLimitation)
        {
            ConfigurationLimitationModel configurationLimitation = new ConfigurationLimitationModel();

            configurationLimitation.Id = tbConfigurationLimitation.id_configuration_limitation;
            if (tbConfigurationLimitation.vl_latitude != null)
                configurationLimitation.Latitude = (double)tbConfigurationLimitation.vl_latitude;
            if (tbConfigurationLimitation.vl_longitude != null)
                configurationLimitation.Longitude = (double)tbConfigurationLimitation.vl_longitude;
            configurationLimitation.Host = tbConfigurationLimitation.ds_host;

            if (tbConfigurationLimitation.tb_limitation_type != null)
                configurationLimitation.LimitationType = new LimitationTypeService().ToModel(tbConfigurationLimitation.tb_limitation_type);

            return configurationLimitation;
        }
    }
}
