using Domain.Entidades;
using Infrastructure.DBConfiguration.EFCore;
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
    public class ProdutoPrecoRepository : Repository<ProdutoPreco>, IProdutoPrecoRepository
    {
        public ProdutoPrecoRepository(ApplicationContext context) : base(context) { }
        public Task<IEnumerable<ProdutoPreco>> GetAllIncludingAsync(    
            Expression<Func<ProdutoPreco, bool>> filter = null,    
            Func<IQueryable<ProdutoPreco>, IOrderedQueryable<ProdutoPreco>> orderBy = null,    
            params string[] includeProperties)        
        {
            IQueryable<ProdutoPreco> query = dbSet;

            return GetAllToListAsync(query, filter, orderBy);
        }

        public Task<ProdutoPreco> GetByIdIncludingAsync(
            Expression<Func<ProdutoPreco, bool>> filter, params string[] includeProperties)
        {
            IQueryable<ProdutoPreco> query = dbSet;

            return GetByIdSingleOrDefaultAsync(query, filter, includeProperties);
        }
    }
}
