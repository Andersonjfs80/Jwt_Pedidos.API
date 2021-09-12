using Application.Interfaces.Standard;
using Infrastructure.Interfaces.Standard;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Standard
{
    public class Service<TEntity> : IService<TEntity> where TEntity : class
    {
        protected readonly IRepository<TEntity> _repository;

        public Service(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual async Task<bool> AddAsync(TEntity obj)
        {
            _repository.AddAsync(obj);
            return await SaveAsync();
        }

        public virtual async Task<bool> Update(TEntity obj)
        {
            _repository.Update(obj);
            return await SaveAsync();
        }

        public virtual async Task<bool> Delete(TEntity obj)
        {
            _repository.Delete(obj);
            return await SaveAsync();
        }

        public virtual async Task<bool> SaveAsync() => await _repository.SaveAsync() > 0 ? true : false;

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await _repository.GetAllAsync();

        public virtual async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> filter) => await _repository.GetByIdAsync(filter);
    }
}
