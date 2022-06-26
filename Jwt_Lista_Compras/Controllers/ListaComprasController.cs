using Application.Interfaces.Domain;
using Domain.Entidades;
using Jwt_Pedidos_v1.API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;


namespace Jwt_Lista_Compras.Controllers
{
    [Route("api/[controller]")]    
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class ListaComprasController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        private readonly IPedidoItemService _pedidoItemService;

        public ListaComprasController(IPedidoService pedidoService, IPedidoItemService pedidoItemService)        {   
            _pedidoService = pedidoService;
            _pedidoItemService = pedidoItemService;
        }
        
       
        [HttpGet]
        public async Task<IActionResult>  ObterListaCompras()
        {             
             return Ok(await _pedidoService.GetAllAsync());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]        
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CriarAtualizarCompras([FromBody] PedidoViewModelDTO pedidoViewModel)
        {
            var pedido = (Pedido)pedidoViewModel;

            try
            {
                if (pedido.PedidoId > 0)
                { await _pedidoService.Update(pedido); }
                else { await _pedidoService.AddAsync(pedido); }

                await _pedidoService.SaveAsync();

                if (pedido.PedidoId <= 0)
                {
                    return NotFound(
                        new NotFoundObjectResult(
                            new
                            {
                                Error = "Erro ao inserir ou atualizar registro.",
                                Message = $"Erro ao atualizar ou inserir registro.",
                                Title = "Inserção ou atualização."
                            }));
                }

                if (pedidoViewModel.PedidoId <= 0)
                {
                    pedidoViewModel.PedidoId = pedido.PedidoId;                    
                }

                foreach (var item in pedidoViewModel.PedidoItens)
                {
                    var pedidoItem = (PedidoItem)item;

                    if (pedidoItem.PedidoId <= 0)
                    {
                        pedidoItem.PedidoId = pedido.PedidoId;
                        item.PedidoId = pedido.PedidoId;
                    }

                    if (pedidoItem.PedidoItemId > 0)
                    { await _pedidoItemService.Update(pedidoItem); }
                    else { await _pedidoItemService.AddAsync(pedidoItem); }

                    await _pedidoItemService.SaveAsync();
                    if (item.PedidoItemId <= 0)
                    { item.PedidoItemId = pedidoItem.PedidoItemId; }
                }

                pedidoViewModel.ValorTotal = pedidoViewModel.PedidoItens.Sum(tot => tot.ValorTotal);
            }
            catch (Exception ex)
            {
                return NotFound(
                    new NotFoundObjectResult(
                        new { Error = "Erro ao inserir ou atualizar registro.", 
                              Message = $"Erro ao atualizar ou inserir registro. Mensagem original: {ex}.", 
                              Title = "Inserção ou atualização." }));
            }

            return CreatedAtAction(
                nameof(CriarAtualizarCompras),
                new { id = pedidoViewModel.PedidoId },
                pedidoViewModel);
        }
    }
}

