using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI_ASPDotNetCore.Contracts
{
    public interface IRepositoryWrapper
    {
        IEmployeeRepository Employee { get; }
        void Save();
    }
}
