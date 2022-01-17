using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WEBAPI_ASPDotNetCore.Models;

namespace WEBAPI_ASPDotNetCore.Repository
{
    public  class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext _reposContext { get; set; }

        public RepositoryBase(RepositoryContext reposContext)
        {
            _reposContext = reposContext;
        }

        public void Create(T entity)
        {
            this._reposContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            this._reposContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return this._reposContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this._reposContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Update(T entity)
        {
            this._reposContext.Set<T>().Update(entity);
        }

        public void UploadImage(T entity)
        {
            this._reposContext.Set<T>().Add(entity);
        }

       
    }
}
