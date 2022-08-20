using Application.Interfaces.Standard;
using Application.ViewModels;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Domain
{
    public interface IPedidoItemService : IService<PedidoItem>
    {
        Task<IEnumerable<PedidoItem>> GetAllIncludingAsync(       
            Expression<Func<PedidoItem, bool>> filter = null,       
            Func<IQueryable<PedidoItem>, IOrderedQueryable<PedidoItem>> orderBy = null,       
            params string[] includeProperties);

        Task<PedidoItem> GetByIdIncludingAsync(
            Expression<Func<PedidoItem, bool>> filter, params string[] includeProperties);

        Task<List<PedidoItemViewModel>> ProcessarPedidoItens(List<PedidoItemViewModel> pedidoItens);
    }
}
