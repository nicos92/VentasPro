using System.Linq.Expressions;
using VentasPro.Domain.Entities;

namespace VentasPro.Domain.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync(bool includeInactive = false);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<T?> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
}