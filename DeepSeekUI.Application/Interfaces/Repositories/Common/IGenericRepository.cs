using System.Linq.Expressions;

namespace DeepSeekUI.Application.Interfaces.Repositories.Common
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task AddArrangeAsync(params T[] entity);
        Task<T?> GetAsync(Guid id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate);
        Task<T> Edit(T entity);
        Task Delete(T entity);
        Task SaveChangesAsync();
    }
}
