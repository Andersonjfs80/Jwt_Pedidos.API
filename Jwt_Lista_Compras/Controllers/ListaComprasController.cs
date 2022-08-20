using Application.Interfaces.Domain;
using Domain.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading.Tasks;
using Jwt_Pedidos_v1.API.Middlewares.Attributes;
using Application.ViewModels;

namespace Jwt_Lista_Compras.Controllers
{

	[Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}/")]   
    public class ListaComprasController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        private readonly IPedidoItemService _pedidoItemService;

        public ListaComprasController(IPedidoService pedidoService, IPedidoItemService pedidoItemService)        
        {   
            _pedidoService = pedidoService;
            _pedidoItemService = pedidoItemService;
        }

        [HttpPost("listas-de-compras")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterListaCompras() => Ok(await _pedidoService.GetAllAsync());

        [HttpPost("lista-de-compras")]
		[ValidarModelState(typeof(PedidoViewModel))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> ObterListaCompras(PedidoViewModel pedidoViewModel) => Ok(await _pedidoService.GetByIdAsync(pedido => pedido.PedidoId == pedidoViewModel.PedidoId));

		// [GlobalExceptionHandler]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[HttpPost("processar-compras")]
		public async Task<IActionResult> ProcessarCompras([FromBody] PedidoViewModel pedidoViewModel) => CreatedAtAction(
				nameof(ProcessarCompras),
				new { id = pedidoViewModel.PedidoId },
				await _pedidoService.ProcessarPedido(pedidoViewModel));

		private async Task InserirOuAtualizarItens(PedidoViewModel pedidoViewModel, Pedido pedido)
        {
            foreach (var item in pedidoViewModel.PedidoItens)
            {
                var pedidoItem = (PedidoItem)item;

                if (pedidoItem.PedidoId <= 0)
                {
                    pedidoItem.PedidoId = pedido.PedidoId;
                    item.PedidoId = pedido.PedidoId;
                }

                if (pedidoItem.PedidoItemId > 0)
                { 
                    _pedidoItemService.Update(pedidoItem);
                }
                else 
                { 
                    await _pedidoItemService.AddAsync(pedidoItem);
                }

                await _pedidoItemService.SaveAsync();
                if (item.PedidoItemId <= 0)
                { 
                    item.PedidoItemId = pedidoItem.PedidoItemId; 
                }
            }
        }
    }
}

