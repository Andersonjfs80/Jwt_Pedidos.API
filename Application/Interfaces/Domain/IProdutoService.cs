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
    public interface IProdutoService : IService<Produto>
    {
        Task<IEnumerable<Produto>> GetAllIncludingAsync(       
            Expression<Func<Produto, bool>> filter = null,       
            Func<IQueryable<Produto>, IOrderedQueryable<Produto>> orderBy = null,       
            params string[] includeProperties);

        Task<Produto> GetByIdIncludingAsync(
            Expression<Func<Produto, bool>> filter, params string[] includeProperties);
    }
}
