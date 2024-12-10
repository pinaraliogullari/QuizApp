namespace QuizAPI.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : class
    {
        Task<bool> CreateAsync(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        Task<int> SaveChangesAsync();

    }
}
