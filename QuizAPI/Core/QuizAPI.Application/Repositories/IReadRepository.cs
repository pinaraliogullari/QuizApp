using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace QuizAPI.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : class
    {
        Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> options = null,Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,bool tracking = true);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> options = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool tracking = true);
        Task<T>GetByIdAsync(string id,bool tracking=true);
    }
}
