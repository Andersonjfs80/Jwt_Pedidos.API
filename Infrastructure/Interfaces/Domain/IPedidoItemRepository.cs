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
    public interface IPedidoItemRepository : IRepository<PedidoItem>
    {
        Task<IEnumerable<PedidoItem>> GetAllIncludingAsync(    
            Expression<Func<PedidoItem, bool>> filter = null,   
            Func<IQueryable<PedidoItem>, IOrderedQueryable<PedidoItem>> orderBy = null,    
            params string[] includeProperties);

        Task<PedidoItem> GetByIdIncludingAsync(
            Expression<Func<PedidoItem, bool>> filter, params string[] includeProperties);
    }
}
