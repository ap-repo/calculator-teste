using Apponto.Database;
using Apponto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apponto.Service
{
    public class ConfigurationService
    {
        public ConfigurationModel Add(ConfigurationModel configuration)
        {
            using (var appontoContext = new AppontoContext())
            {
                tb_configuration tbConfiguration = ToDbModel(configuration);
                appontoContext.tb_configuration.Add(tbConfiguration);
                appontoContext.SaveChanges();

                ConfigurationModel ret = ToModel(tbConfiguration);

                return ret;
            }
        }

        private tb_configuration ToDbModel(ConfigurationModel configuration)
        {
            tb_configuration tbConfiguration = new tb_configuration();

            if (configuration.ConfigurationLimitation != null)
                tbConfiguration.tb_configuration_limitation_id_configuration_limitation = configuration.ConfigurationLimitation.Id;

            return tbConfiguration;
        }

        public ConfigurationModel ToModel(tb_configuration tbConfiguration)
        {
            ConfigurationModel configuration = new ConfigurationModel();

            configuration.Id = tbConfiguration.id_configuration;

            if(tbConfiguration.tb_configuration_limitation != null)
                configuration.ConfigurationLimitation = new ConfigurationLimitationService().ToModel(tbConfiguration.tb_configuration_limitation);

            return configuration;
        }
    }
}
