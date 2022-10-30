using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Standard
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> filter);
        Task AddAsync(TEntity entity);
        Task AddAsync(IEnumerable<TEntity> entity);
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
