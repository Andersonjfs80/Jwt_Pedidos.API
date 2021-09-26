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
    public class CategoriaItemRepository : Repository<CategoriaItem>, ICategoriaItemRepository
    {
        public CategoriaItemRepository(ApplicationContext context) : base(context) { }
        public Task<IEnumerable<CategoriaItem>> GetAllIncludingAsync(    
            Expression<Func<CategoriaItem, bool>> filter = null,    
            Func<IQueryable<CategoriaItem>, IOrderedQueryable<CategoriaItem>> orderBy = null,    
            params string[] includeProperties)        
        {
            IQueryable<CategoriaItem> query = dbSet;

            return GetAllToListAsync(query, filter, orderBy);
        }

        public Task<CategoriaItem> GetByIdIncludingAsync(
            Expression<Func<CategoriaItem, bool>> filter, params string[] includeProperties)
        {
            IQueryable<CategoriaItem> query = dbSet;

            return GetByIdSingleOrDefaultAsync(query, filter, includeProperties);
        }
    }
}
