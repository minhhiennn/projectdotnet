using Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao.Admin
{
    public class CompanyDaoAdmin
    {
        private ProjectDotNetDbContext DbContext = null;
        public CompanyDaoAdmin()
        {
            DbContext = new ProjectDotNetDbContext();
        }
        public IEnumerable<Framework.Company> getAllCompany()
        {
            return DbContext.Companies;
        }
    }
}