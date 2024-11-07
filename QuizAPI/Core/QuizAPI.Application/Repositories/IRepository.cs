using Microsoft.EntityFrameworkCore;

namespace QuizAPI.Application.Repositories
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> Table {  get; }
    }
}
