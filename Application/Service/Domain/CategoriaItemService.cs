using Application.Interfaces.Domain;
using Application.Service.Standard;
using Domain.Entidades;
using Infrastructure.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Domain
{
    public class CategoriaItemService : Service<CategoriaItem>, ICategoriaItemService
    {
        private readonly new ICategoriaItemRepository _repository;

        public CategoriaItemService(ICategoriaItemRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<CategoriaItem>> GetAllIncludingAsync(Expression<Func<CategoriaItem, bool>> filter = null, Func<IQueryable<CategoriaItem>, IOrderedQueryable<CategoriaItem>> orderBy = null, params string[] includeProperties)
        {
            return await _repository.GetAllIncludingAsync(filter, orderBy, includeProperties: new string[] { nameof(CategoriaItem.Categoria) });
        }

        public async Task<CategoriaItem> GetByIdIncludingAsync(Expression<Func<CategoriaItem, bool>> filter, params string[] includeProperties)
        {
            return await _repository.GetByIdIncludingAsync(filter, includeProperties: new string[] { nameof(CategoriaItem.Categoria) });
        }
    }
}
