using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI_ASPDotNetCore.Contracts;
using WEBAPI_ASPDotNetCore.Models;

namespace WEBAPI_ASPDotNetCore.Repository
{
    public class ImageReposWrapper : IImageReposWrapper
    {
        private RepositoryContext _repoContext;
        private IImageRepository _imgRepos;
        public ImageReposWrapper(RepositoryContext repoContext)
        {
            _repoContext = repoContext;
        }

        public IImageRepository IMG
        {
            get
            {
                if (_imgRepos == null)
                {
                    _imgRepos = new ImageRepository(_repoContext);
                }

                return _imgRepos;
            }
        }

        public void Save()
        {
            this._repoContext.SaveChanges();
        }
    }
}
