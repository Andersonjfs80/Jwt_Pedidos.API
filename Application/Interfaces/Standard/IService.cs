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
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Update(IEnumerable<TEntity> entity);
        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entity);
        Task<bool> SaveAsync();
        Task StartTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
