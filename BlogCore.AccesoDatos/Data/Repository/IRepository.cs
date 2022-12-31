using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BlogCore.AccesoDatos.Data.Repository
{
    //<T> va a ser una clase que se va a implementar desde otro archivo, en este caso llamado Repository
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        //Explicacion class 121
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null
            );
        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
            );
        void Add(T entity);
        void Remove(int id);
        void Remove(T entity);
    }
}
