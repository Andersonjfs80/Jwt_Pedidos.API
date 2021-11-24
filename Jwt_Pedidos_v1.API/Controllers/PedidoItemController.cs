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
        public ActionResult<IEnumerable<PedidoItem>> GetAllByPedidoId(int pedidoId)
        {
            var pedidoItens = _pedidoItemService.GetAllIncludingAsync(p => p.PedidoId == pedidoId);

            return Ok(JsonSerializer.Serialize(pedidoItens, new JsonSerializerOptions()
            {
                MaxDepth = 0,
                IgnoreNullValues = true,
                IgnoreReadOnlyProperties = true
            }));
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<PedidoItem> GetById(int id)
        {
            var pedido = _pedidoItemService.GetByIdAsync(p => p.PedidoItemId == id).Result;

            if (pedido is null)
                return NotFound();

            return Ok(JsonSerializer.Serialize(pedido, new JsonSerializerOptions()
            {
                MaxDepth = 0,
                IgnoreNullValues = true,
                IgnoreReadOnlyProperties = true
            }));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Create(PedidoItem pedidoItem)
        {
            if (_pedidoItemService.AddAsync(pedidoItem).Result == false)
                return NotFound();

            return CreatedAtAction(nameof(Create), pedidoItem);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Update(int id, PedidoItem pedidoItem)
        {
            if (id <= 0)
                return NotFound();

            if (pedidoItem is null)
                return NotFound();

            if (pedidoItem.PedidoId != id)
                return NotFound();

            if (_pedidoItemService.Update(pedidoItem).Result == false)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Delete(int id)
        {
            var existingPedidoItem = _pedidoItemService.GetByIdAsync(p => p.PedidoItemId == id).Result;

            if (existingPedidoItem is null)
                return NotFound();

            if (_pedidoItemService.Delete(existingPedidoItem).Result == false)
                return NotFound();

            return NoContent();
        }
    }
}
