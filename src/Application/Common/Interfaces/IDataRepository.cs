using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IDataRepository
    {
        Task<T> GetFirst<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task Create<T>(T entity) where T : class;
        IQueryable<T> Query<T>() where T : class;
    }
}
