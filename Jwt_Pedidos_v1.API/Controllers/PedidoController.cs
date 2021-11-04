using Application.Interfaces.Domain;
using Domain.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Jwt_Pedidos_v1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
                IgnoreNullValues = true,
                IgnoreReadOnlyProperties = true
            }));
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<Pedido> Get(int id)
        {
            var pedido = _pedidoService.GetByIdAsync(p => p.PedidoId == id).Result;

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
        public IActionResult Create(Pedido pedido)
        {
            if (_pedidoService.AddAsync(pedido).Result == false)
                return NotFound();

            return CreatedAtAction(nameof(Create), pedido);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Update(int id, Pedido pedido)
        {
            if (id <= 0)
                return NotFound();

            if (pedido is null)
                return NotFound();

            if (pedido.PedidoId != id)
                return NotFound();

            if (_pedidoService.Update(pedido).Result == false)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Delete(int id)
        {
            var existingPedido = _pedidoService.GetByIdAsync(p => p.PedidoId == id).Result;

            if (existingPedido is null)
                return NotFound();

            if (_pedidoService.Delete(existingPedido).Result == false)
                return NotFound();

            return NoContent();
        }
    }
}
