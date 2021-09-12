using Domain.Entidades;
using Infrastructure.Interfaces.Domain;
using Infrastructure.Repositories.Standard.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Domain
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public Task<IEnumerable<Produto>> GetAllIncludingAsync(    
            Expression<Func<Produto, bool>> filter = null,    
            Func<IQueryable<Produto>, IOrderedQueryable<Produto>> orderBy = null,    
            params string[] includeProperties)        
        {
            IQueryable<Produto> query = dbSet;

            return GetAllToListAsync(query, filter, orderBy);
        }

        public Task<Produto> GetByIdIncludingAsync(
            Expression<Func<Produto, bool>> filter, params string[] includeProperties)
        {
            IQueryable<Produto> query = dbSet;

            return GetByIdSingleOrDefaultAsync(query, filter, includeProperties);
        }
    }
}
