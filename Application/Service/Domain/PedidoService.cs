using Application.Common;
using Application.Interfaces.Domain;
using Application.Service.Standard;
using Application.ViewModels;
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
		
		public async Task<PedidoViewModel> ProcessarPedido(PedidoViewModel pedidoViewModel)
        {
            if (pedidoViewModel == null) 
            {
				throw new NaoEncontradoException
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
			if (pedido.PedidoId > 0)
			{
				Update(pedido);
			}
			else
			{
				await AddAsync(pedido);
			}

			await SaveAsync();

			pedidoViewModel.PedidoId = pedido.PedidoId;
			pedidoViewModel.PedidoItens.ForEach(itens => itens.PedidoId = pedido.PedidoId);

			if (pedidoViewModel.PedidoItens.Any())
			{	
				await _pedidoItemService.ProcessarPedidoItens(pedidoViewModel.PedidoItens);
			}

			try
			{
				await CommitAsync();			
			}
			catch (Exception)
			{
				await RollbackAsync();			
			}

			pedidoViewModel.ValorTotal = pedidoViewModel.PedidoItens.Sum(tot => tot.ValorTotal);
			return pedidoViewModel;
		}
    }
}
