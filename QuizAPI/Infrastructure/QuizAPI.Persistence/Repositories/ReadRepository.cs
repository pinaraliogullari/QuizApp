using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using QuizAPI.Application.Repositories;
using QuizAPI.Domain.Common;
using QuizAPI.Persistence.Context;
using System.Linq.Expressions;

namespace QuizAPI.Persistence.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
{
    private readonly QuizAppDbContext _context;

    public ReadRepository(QuizAppDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public async Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> options = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool tracking = true)
    {
        IQueryable<T> query = Table;
        if (!tracking)
            query = query.AsNoTracking();

        if (include!=null)
            query = include(query);

        if (options!=null)
            query = query.Where(options);

        return await query.ToListAsync();
    }

    public async Task<T> GetByIdAsync(string id, bool tracking = true)
    {
        IQueryable<T> query = Table;
        if (!tracking)
            query = query.AsNoTracking();
        return await query.SingleOrDefaultAsync(x=>x.Id==int.Parse(id));
    }

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> options = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool tracking = true)
    {
        IQueryable<T> query = Table;
        if (!tracking)
            query = query.AsNoTracking();

        if (include != null)
            query = include(query);

        if (options != null)
            query = query.Where(options);

        return await query.FirstOrDefaultAsync();
    }
}
