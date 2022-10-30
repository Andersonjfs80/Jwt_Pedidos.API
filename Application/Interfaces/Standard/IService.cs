using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Standard
{
    public interface IService<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> filter);
        Task<bool> AddAsync(TEntity entity);
        Task<bool> AddAsync(IEnumerable<TEntity> entity);        
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> UpdateAsync(IEnumerable<TEntity> entity);
        Task<bool> DeleteAsync(TEntity entity);
        Task<bool> DeleteAsync(IEnumerable<TEntity> entity);
        Task StartTransactionAsync();
        Task CommitAsync();
    }
}
