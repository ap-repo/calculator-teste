using Apponto.Database;
using Apponto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apponto.Service
{
    public class CompanyService
    {
        public CompanyModel Add(CompanyModel  company)
        {
            using (var appontoContext = new AppontoContext())
            {
                tb_company tbCompany = ToDbModel(company);
                appontoContext.tb_company.Add(tbCompany);
                appontoContext.SaveChanges();

                CompanyModel ret = ToModel(tbCompany);

                return ret;
            }
        }

        public CompanyModel Get(string token)
        {
            using (var appontoContext = new AppontoContext())
            {
                tb_company tbCompany = appontoContext.tb_company.Where(x => x.ds_token == token).FirstOrDefault();

                return ToModel(tbCompany);
            }
        }

        public tb_company ToDbModel(CompanyModel company)
        {
            tb_company tbCompany = new tb_company();

            tbCompany.ds_name = company.Name;
            tbCompany.ds_token = company.Token;

            return tbCompany;
        }

        public CompanyModel ToModel(tb_company tbCompany)
        {
            CompanyModel company = new CompanyModel();

            company.Id = tbCompany.id_company;
            company.Name = tbCompany.ds_name;
            company.Token = tbCompany.ds_token;

            return company;
        }
    }
}
