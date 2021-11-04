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
    public class PedidoItemRepository : Repository<PedidoItem>, IPedidoItemRepository
    {
        public PedidoItemRepository(ApplicationContext context) : base(context) { }
        public Task<IEnumerable<PedidoItem>> GetAllIncludingAsync(    
            Expression<Func<PedidoItem, bool>> filter = null,    
            Func<IQueryable<PedidoItem>, IOrderedQueryable<PedidoItem>> orderBy = null,    
            params string[] includeProperties)        
        {
            IQueryable<PedidoItem> query = dbSet;

            return GetAllToListAsync(query, filter, orderBy);
        }

        public Task<PedidoItem> GetByIdIncludingAsync(
            Expression<Func<PedidoItem, bool>> filter, params string[] includeProperties)
        {
            IQueryable<PedidoItem> query = dbSet;

            return GetByIdSingleOrDefaultAsync(query, filter, includeProperties);
        }
    }
}
