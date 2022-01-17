using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI_ASPDotNetCore.Contracts;
using WEBAPI_ASPDotNetCore.Models;

namespace WEBAPI_ASPDotNetCore.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IEmployeeRepository _employee;

        public RepositoryWrapper(RepositoryContext repoContext)
        {
            _repoContext = repoContext;
        }

        public IEmployeeRepository Employee
        {
            get
            {
                if(_employee == null)
                {
                    _employee = new EmployeeRepository(_repoContext);
                }

                return _employee;                    
            }
        }

      

        public void Save()
        {
            this._repoContext.SaveChanges();
        }
    }
}
