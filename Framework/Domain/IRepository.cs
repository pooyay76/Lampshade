using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Framework.Domain
{
    public interface IRepository<Tkey,T> where T:class
    {
        public T Get(Tkey id);
        public List<T> List();
        public void Create(T entity);
        public bool Exists(Expression<Func<T,bool>> expression);
        public void SaveChanges();

    }
}
