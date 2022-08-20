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

		public async Task AddAsync(TEntity obj)
		{
			await _repository.AddAsync(obj);
		}

		public void Update(TEntity obj)
        {
            _repository.Update(obj);
        }

        public void Update(IEnumerable<TEntity> obj)
        {
            _repository.Update(obj);           
        }

        public void Delete(TEntity obj)
        {
            _repository.Delete(obj);
        }

        public void Delete(IEnumerable<TEntity> obj)
        {
            _repository.Delete(obj);
        }

        public virtual async Task<bool> SaveAsync() => await _repository.SaveAsync();

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await _repository.GetAllAsync();

        public virtual async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> filter) => await _repository.GetByIdAsync(filter);

		public async Task StartTransactionAsync()
		{
			await _repository.StartTransactionAsync();    
		}

		public async Task CommitAsync()
		{
            await _repository.CommitAsync();
        }

		public async Task RollbackAsync()
		{
            await _repository.RollbackAsync();
        }
	}
}
