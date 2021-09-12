using Application.Interfaces.Standard;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Domain
{
    public interface IProdutoPrecoService : IService<ProdutoPreco>
    {
        Task<IEnumerable<ProdutoPreco>> GetAllIncludingAsync(       
            Expression<Func<ProdutoPreco, bool>> filter = null,       
            Func<IQueryable<ProdutoPreco>, IOrderedQueryable<ProdutoPreco>> orderBy = null,       
            params string[] includeProperties);

        Task<ProdutoPreco> GetByIdIncludingAsync(
            Expression<Func<ProdutoPreco, bool>> filter, params string[] includeProperties);
    }
}
