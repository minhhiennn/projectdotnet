using Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao.User
{
    public class CompanyDao
    {
        private ProjectDotNetDbContext DbContext = null;
        public CompanyDao()
        {
            DbContext = new ProjectDotNetDbContext();
        }

        //lấy company bằng id
        public Company GetCompanyById(int id)
        {
            return DbContext.Companies.SingleOrDefault(x => x.id == id);
        }
    }
}
