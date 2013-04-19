using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Qoveo.Impact.Data
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
