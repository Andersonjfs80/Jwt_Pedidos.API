using Application.Interfaces.Standard;
using Domain.ViewModels;
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
            Func<IQueryable<PedidoItem>, IOrderedQueryable<PedidoItem>> orderBy = null);

        Task<PedidoItem> GetByIdIncludingAsync(
            Expression<Func<PedidoItem, bool>> filter);

        Task<List<PedidoItemViewModel>> ProcessarPedidoItens(List<PedidoItemViewModel> pedidoItens);
    }
}
