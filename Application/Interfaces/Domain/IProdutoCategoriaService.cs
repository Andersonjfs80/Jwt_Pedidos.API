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
    public interface IProdutoCategoriaService : IService<ProdutoCategoria>
    {
        Task<IEnumerable<ProdutoCategoria>> GetAllIncludingAsync(       
            Expression<Func<ProdutoCategoria, bool>> filter = null,       
            Func<IQueryable<ProdutoCategoria>, IOrderedQueryable<ProdutoCategoria>> orderBy = null,       
            params string[] includeProperties);

        Task<ProdutoCategoria> GetByIdIncludingAsync(
            Expression<Func<ProdutoCategoria, bool>> filter, params string[] includeProperties);
    }
}
