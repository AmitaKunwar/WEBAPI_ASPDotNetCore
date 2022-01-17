using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WEBAPI_ASPDotNetCore.Contracts;
using WEBAPI_ASPDotNetCore.Models;

namespace WEBAPI_ASPDotNetCore.Repository
{
    public class ImageRepository : RepositoryBase<LoadImage>, IImageRepository
    {
        public ImageRepository(RepositoryContext _reposContext)
            : base(_reposContext)
        {
        }
       
        
    }
}
