using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI_ASPDotNetCore.Models;
using WEBAPI_ASPDotNetCore.Repository;

namespace WEBAPI_ASPDotNetCore.Contracts
{
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {
    }
}
