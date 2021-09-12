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
    public class ProdutoService : Service<Produto>, IProdutoService
    {
        private readonly new IProdutoRepository _repository;

        public ProdutoService(IProdutoRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Produto>> GetAllIncludingAsync(Expression<Func<Produto, bool>> filter = null, Func<IQueryable<Produto>, IOrderedQueryable<Produto>> orderBy = null, params string[] includeProperties)
        {
            return await _repository.GetAllIncludingAsync(filter, orderBy, includeProperties: new string[] { nameof(Produto.ProdutoPreco) });
        }

        public async Task<Produto> GetByIdIncludingAsync(Expression<Func<Produto, bool>> filter, params string[] includeProperties)
        {
            return await _repository.GetByIdIncludingAsync(filter, includeProperties: new string[] { nameof(Produto.ProdutoPreco) });
        }
    }
}
