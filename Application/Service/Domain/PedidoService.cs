using Domain.ViewModels.CustomExceptions;
using Application.Interfaces.Domain;
using Application.Service.Standard;
using Domain.ViewModels;
using Domain.Entidades;
using Infrastructure.Interfaces.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Service.Domain
{
	public class PedidoService : Service<Pedido>, IPedidoService
    {
		private readonly IPedidoItemService _pedidoItemService;
		public PedidoService(IPedidoRepository repository, IPedidoItemService pedidoItemService) : base(repository)
		{
			_pedidoItemService = pedidoItemService;
		}

		private async Task<bool> AddOrUpdateAsync(Pedido pedido) 
		{
			if (pedido.PedidoId > 0)
			{
				return await UpdateAsync(pedido);
			}

			return await AddAsync(pedido);
		}

		public async Task<PedidoViewModel> ProcessarPedido(PedidoViewModel pedidoViewModel)
        {
            if (pedidoViewModel == null) 
            {
				throw new NotFoundException
				{
					StatusCode = HttpStatusCode.NotFound,
					Mensagem = JsonSerializer.Serialize(
                        new { 
                            Error = "Erro de validação.", 
                            Message = $"Erro objeto vazio.", 
                            Title = "Erro pedido não encontrado." 
                        })
				};
			}

			var pedido = (Pedido)pedidoViewModel;
			
			await StartTransactionAsync();
			await AddOrUpdateAsync(pedido);

			pedidoViewModel.PedidoId = pedido.PedidoId;
			pedidoViewModel.PedidoItens.ForEach(itens => itens.PedidoId = pedido.PedidoId);

			if (pedidoViewModel.PedidoItens.Any())
			{	
				await _pedidoItemService.ProcessarPedidoItens(pedidoViewModel.PedidoItens);
			}

			await CommitAsync();

			pedidoViewModel.ValorTotal = pedidoViewModel.PedidoItens.Sum(tot => tot.ValorTotal);
			return pedidoViewModel;
		}
    }
}
