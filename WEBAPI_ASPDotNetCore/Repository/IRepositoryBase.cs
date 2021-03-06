using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WEBAPI_ASPDotNetCore.Repository
{
    public interface IRepositoryBase<T>
    {
        public IQueryable<T> FindAll();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        public void Create(T entity);
        public void Update(T entity);
        public void Delete(T entity);

        public void UploadImage(T entity);
    }
}
