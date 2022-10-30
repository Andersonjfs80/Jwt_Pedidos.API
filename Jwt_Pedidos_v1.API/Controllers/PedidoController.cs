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
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _pedidoService.GetAllAsync());
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _pedidoService.GetByIdAsync(p => p.PedidoId == id));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Create(Pedido pedido)
        {            
            if (!await _pedidoService.AddAsync(pedido))
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
                        
            if (!await _pedidoService.UpdateAsync(pedido))
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
                        
            if (!await _pedidoService.DeleteAsync(existingPedido))
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
