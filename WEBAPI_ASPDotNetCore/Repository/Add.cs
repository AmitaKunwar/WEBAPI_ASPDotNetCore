using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI_ASPDotNetCore.Repository
{
    public class Add : IAdd
    {
        public int add(int a, int b)
        {
            return a + b;
        }
    }
}
