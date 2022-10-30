using Application.Interfaces.Domain;
using Application.Service.Standard;
using Domain.ViewModels;
using Domain.Entidades;
using Infrastructure.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Domain
{
    public class PedidoItemService : Service<PedidoItem>, IPedidoItemService
    {
        private readonly new IPedidoItemRepository _repository;

        public PedidoItemService(IPedidoItemRepository repository) : base(repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<PedidoItem>> GetAllIncludingAsync(
            Expression<Func<PedidoItem, bool>> filter = null, 
            Func<IQueryable<PedidoItem>, IOrderedQueryable<PedidoItem>> orderBy = null)
        {
            return await _repository.GetAllIncludingAsync(
                filter, 
                orderBy, 
                includeProperties: new string[] { 
                    nameof(PedidoItem.Produto), 
                    nameof(PedidoItem.Unidade), 
                    nameof(PedidoItem.TabelaPreco) });
        }

        public async Task<PedidoItem> GetByIdIncludingAsync(
            Expression<Func<PedidoItem, bool>> filter)
        {
            return await _repository.GetByIdIncludingAsync(
                filter, 
                includeProperties: new string[] { 
                    nameof(PedidoItem.Produto), 
                    nameof(PedidoItem.Unidade), 
                    nameof(PedidoItem.TabelaPreco) });
        }

        private async Task<bool> AddOrUpdateAsync(PedidoItem pedidoItem)
        {
            if (pedidoItem.PedidoItemId > 0)
            {
                return await UpdateAsync(pedidoItem);
            }

            return await AddAsync(pedidoItem);
        }

        public async Task<List<PedidoItemViewModel>> ProcessarPedidoItens(List<PedidoItemViewModel> pedidoItensViewModel)
        {   
			foreach (var pedidoItemViewModel in pedidoItensViewModel)
			{
                var pedidoItem = (PedidoItem)pedidoItemViewModel;
                await AddOrUpdateAsync(pedidoItem);

                pedidoItemViewModel.PedidoItemId = pedidoItem.PedidoItemId;
            }

            return pedidoItensViewModel;
        }
    }
}
