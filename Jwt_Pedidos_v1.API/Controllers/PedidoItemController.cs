using Application.Interfaces.Domain;
using Domain.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Jwt_Pedidos_v1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class PedidoItemController : Controller
    {
        private readonly IPedidoItemService _pedidoItemService;
        public PedidoItemController(IPedidoItemService pedidoItemService)
        {
            _pedidoItemService = pedidoItemService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAllByPedidoId(int pedidoId)
        {
            return Ok(await _pedidoItemService.GetAllIncludingAsync(p => p.PedidoId == pedidoId));
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetById(int id)
        {
            var pedido = await _pedidoItemService.GetByIdAsync(p => p.PedidoItemId == id);

            if (pedido is null)
                return NotFound();

            return Ok(pedido);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Create(PedidoItem pedidoItem)
        {            
            if (!await _pedidoItemService.AddAsync(pedidoItem) == false)
                return NotFound();

            return CreatedAtAction(nameof(Create), pedidoItem);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(int id, PedidoItem pedidoItem)
        {
            if (id <= 0)
                return NotFound();

            if (pedidoItem is null)
                return NotFound();

            if (pedidoItem.PedidoId != id)
                return NotFound();
                        
            if (!await _pedidoItemService.UpdateAsync(pedidoItem) == false)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(int id)
        {
            var existingPedidoItem = await _pedidoItemService.GetByIdAsync(p => p.PedidoItemId == id);

            if (existingPedidoItem is null)
                return NotFound();
                        
            if (!await _pedidoItemService.DeleteAsync(existingPedidoItem) == false)
                return NotFound();

            return NoContent();
        }
    }
}
