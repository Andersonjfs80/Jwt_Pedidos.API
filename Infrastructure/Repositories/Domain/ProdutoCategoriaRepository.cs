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
    public class ProdutoCategoriaRepository : Repository<ProdutoCategoria>, IProdutoCategoriaRepository
    {
        public ProdutoCategoriaRepository(ApplicationContext context) : base(context) { }
        public Task<IEnumerable<ProdutoCategoria>> GetAllIncludingAsync(    
            Expression<Func<ProdutoCategoria, bool>> filter = null,    
            Func<IQueryable<ProdutoCategoria>, IOrderedQueryable<ProdutoCategoria>> orderBy = null,    
            params string[] includeProperties)        
        {
            IQueryable<ProdutoCategoria> query = dbSet;

            return GetAllToListAsync(query, filter, orderBy);
        }

        public Task<ProdutoCategoria> GetByIdIncludingAsync(
            Expression<Func<ProdutoCategoria, bool>> filter, params string[] includeProperties)
        {
            IQueryable<ProdutoCategoria> query = dbSet;

            return GetByIdSingleOrDefaultAsync(query, filter, includeProperties);
        }
    }
}
