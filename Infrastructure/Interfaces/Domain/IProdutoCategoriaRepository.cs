using Domain.Entidades;
using Infrastructure.Interfaces.Standard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Domain
{
    public interface IProdutoCategoriaRepository : IRepository<ProdutoCategoria>
    {
        Task<IEnumerable<ProdutoCategoria>> GetAllIncludingAsync(    
            Expression<Func<ProdutoCategoria, bool>> filter = null,   
            Func<IQueryable<ProdutoCategoria>, IOrderedQueryable<ProdutoCategoria>> orderBy = null,    
            params string[] includeProperties);

        Task<ProdutoCategoria> GetByIdIncludingAsync(
            Expression<Func<ProdutoCategoria, bool>> filter, params string[] includeProperties);
    }
}
