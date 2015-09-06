using Apponto.Database;
using Apponto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apponto.Service
{
    public class LimitationTypeService
    {
        private tb_limitation_type ToDbModel(LimitationTypeModel limitationType)
        {
            tb_limitation_type tbLimitationType = new tb_limitation_type();

            tbLimitationType.id_limitation_type = limitationType.Id;
            tbLimitationType.ds_limitation_type = limitationType.Name;

            return tbLimitationType;
        }

        public LimitationTypeModel ToModel(tb_limitation_type tbLimitationType)
        {
            LimitationTypeModel limitationTypeModel = new LimitationTypeModel();

            limitationTypeModel.Id = tbLimitationType.id_limitation_type;
            limitationTypeModel.Name = tbLimitationType.ds_limitation_type;

            return limitationTypeModel;
        }
    }
}
