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
    public class ProdutoPrecoService : Service<ProdutoPreco>, IProdutoPrecoService
    {
        private readonly new IProdutoPrecoRepository _repository;

        public ProdutoPrecoService(IProdutoPrecoRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<ProdutoPreco>> GetAllIncludingAsync(Expression<Func<ProdutoPreco, bool>> filter = null, Func<IQueryable<ProdutoPreco>, IOrderedQueryable<ProdutoPreco>> orderBy = null, params string[] includeProperties)
        {
            return await _repository.GetAllIncludingAsync(filter, orderBy, includeProperties: new string[] { nameof(ProdutoPreco.TabelaPreco), nameof(ProdutoPreco.Unidade) });
        }

        public async Task<ProdutoPreco> GetByIdIncludingAsync(Expression<Func<ProdutoPreco, bool>> filter, params string[] includeProperties)
        {
            return await _repository.GetByIdIncludingAsync(filter, includeProperties: new string[] { nameof(ProdutoPreco.TabelaPreco), nameof(ProdutoPreco.Unidade) });
        }
    }
}
