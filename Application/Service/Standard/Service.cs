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

		public async Task<bool> AddAsync(TEntity obj)
		{
			await _repository.AddAsync(obj);
            return await _repository.SaveAsync();
        }

        public async Task<bool> AddAsync(IEnumerable<TEntity> obj)
        {
            await _repository.AddAsync(obj);
            return await _repository.SaveAsync();
        }

        public async Task<bool> UpdateAsync(TEntity obj)
        {
            _repository.Update(obj);
            return await _repository.SaveAsync();
        }

        public async Task<bool> UpdateAsync(IEnumerable<TEntity> obj)
        {
            _repository.Update(obj);
            return await _repository.SaveAsync();
        }

        public async Task<bool> DeleteAsync(TEntity obj)
        {
            _repository.Delete(obj);
            return await _repository.SaveAsync();
        }

        public async Task<bool> DeleteAsync(IEnumerable<TEntity> obj)
        {
            _repository.Delete(obj);
            return await _repository.SaveAsync();
        }      

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await _repository.GetAllAsync();

        public virtual async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> filter) => await _repository.GetByIdAsync(filter);

		public async Task StartTransactionAsync()
		{
			await _repository.StartTransactionAsync();    
		}

		public async Task CommitAsync()
		{         
            try
            {
                await _repository.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repository.RollbackAsync();
                throw new Exception($"Erro ao executar transação {ex}");          
            }
        }
	}
}
