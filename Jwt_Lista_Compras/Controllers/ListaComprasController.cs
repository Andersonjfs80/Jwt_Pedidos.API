using Application.Interfaces.Domain;
using Domain.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading.Tasks;
using Jwt_Pedidos_v1.API.Middlewares.Attributes;
using Domain.ViewModels;

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

		public ListaComprasController(IPedidoService pedidoService) => _pedidoService = pedidoService;

		[HttpPost("list-all-purchases")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetListAllPurchases() => 
            Ok(await _pedidoService.GetAllAsync());

        [HttpPost("list-purchases")]
		[ValidarModelState(typeof(PedidoViewModel))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> ListPurchases(PedidoIdViewModel pedidoIdViewModel) =>
            Ok(await _pedidoService.GetByIdAsync(pedido => pedido.PedidoId == pedidoIdViewModel.PedidoId));

		// [GlobalExceptionHandler]
		[ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
		[HttpPost("process-purchases")]
		public async Task<IActionResult> ProcessPurchases([FromBody] PedidoViewModel pedidoViewModel) => CreatedAtAction(
				nameof(ProcessPurchases),
				new { id = pedidoViewModel.PedidoId },
				await _pedidoService.ProcessarPedido(pedidoViewModel));
    }
}

