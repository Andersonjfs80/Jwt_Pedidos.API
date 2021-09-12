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
    public class ProdutoCategoriaService : Service<ProdutoCategoria>, IProdutoCategoriaService
    {
        private readonly new IProdutoCategoriaRepository _repository;

        public ProdutoCategoriaService(IProdutoCategoriaRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<ProdutoCategoria>> GetAllIncludingAsync(Expression<Func<ProdutoCategoria, bool>> filter = null, Func<IQueryable<ProdutoCategoria>, IOrderedQueryable<ProdutoCategoria>> orderBy = null, params string[] includeProperties)
        {
            return await _repository.GetAllIncludingAsync(filter, orderBy, includeProperties: new string[] { nameof(ProdutoCategoria.Produto), nameof(ProdutoCategoria.CategoriaItem), nameof(ProdutoCategoria.CategoriaItem) });
        }

        public async Task<ProdutoCategoria> GetByIdIncludingAsync(Expression<Func<ProdutoCategoria, bool>> filter, params string[] includeProperties)
        {
            return await _repository.GetByIdIncludingAsync(filter, includeProperties: new string[] { nameof(ProdutoCategoria.Produto), nameof(ProdutoCategoria.CategoriaItem), nameof(ProdutoCategoria.CategoriaItem) });
        }
    }
}
