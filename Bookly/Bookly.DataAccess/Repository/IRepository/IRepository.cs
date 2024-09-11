using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bookly.DataAccess.Repository.IRepository
{
    public interface IRepository <T> where T : class
    {
        //T-Category
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter=null,string inculudeProperties = null);
        T Get(Expression<Func<T, bool>> filter, string inculudeProperties = null);
        void Remove(T entity);
        void Add(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
