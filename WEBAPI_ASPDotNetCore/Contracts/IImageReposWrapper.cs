using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI_ASPDotNetCore.Contracts
{
    public interface IImageReposWrapper
    {
        public IImageRepository IMG { get; }
        public void Save();
    }
}
