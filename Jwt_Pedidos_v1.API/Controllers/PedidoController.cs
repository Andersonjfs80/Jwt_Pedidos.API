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
    public class PedidoController : Controller
    {
        private readonly IPedidoService _pedidoService;
        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<IEnumerable<Pedido>> GetAll()
        {
            var pedidos = _pedidoService.GetAllAsync();

            return Ok(JsonSerializer.Serialize(pedidos, new JsonSerializerOptions()
            {
                MaxDepth = 0,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                IgnoreReadOnlyProperties = true
            }));
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<Pedido> Get(int id)
        {
            var pedido = _pedidoService.GetByIdAsync(p => p.PedidoId == id).Result;

            if (pedido is null)
            {
                return NotFound();
            }

            return Ok(JsonSerializer.Serialize(pedido, new JsonSerializerOptions()
            {
                MaxDepth = 0,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                IgnoreReadOnlyProperties = true
            }));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Create(Pedido pedido)
        {
            await _pedidoService.AddAsync(pedido);
            if (!await _pedidoService.SaveAsync())
			{
                return NotFound();
            }

            return CreatedAtAction(nameof(Create), pedido);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(int id, Pedido pedido)
        {
            if (id <= 0)
                return NotFound();

            if (pedido is null)
                return NotFound();

            if (pedido.PedidoId != id)
                return NotFound();

            _pedidoService.Update(pedido);
            if (!await _pedidoService.SaveAsync())
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(int id)
        {
            var existingPedido = _pedidoService.GetByIdAsync(p => p.PedidoId == id).Result;

            if (existingPedido is null)
                return NotFound();

            _pedidoService.Delete(existingPedido);
            if (!await _pedidoService.SaveAsync())
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
