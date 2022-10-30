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
    public class ProdutoPrecoController : ControllerBase
    {
        private readonly IProdutoPrecoService _produtoPrecoService;
        public ProdutoPrecoController(IProdutoPrecoService produtoPrecoService)
        {
            _produtoPrecoService = produtoPrecoService;            
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _produtoPrecoService.GetAllAsync());
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _produtoPrecoService.GetByIdAsync(pc => pc.ProdutoPrecoId == id));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Create(ProdutoPreco produtoPreco)
        {           
            if (await _produtoPrecoService.AddAsync(produtoPreco) == false)
                return NotFound();

            return CreatedAtAction(nameof(Create), produtoPreco);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(int id, ProdutoPreco produtoPreco)
        {
            if (id <= 0)
                return NotFound();

            if (produtoPreco is null)
                return NotFound();

            if (produtoPreco.ProdutoPrecoId != id)
                return NotFound();
                        
            if (!await _produtoPrecoService.UpdateAsync(produtoPreco) == false)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(int id)
        {
            var existingProdutoPreco = await _produtoPrecoService.GetByIdAsync(pc => pc.ProdutoPrecoId == id);

            if (existingProdutoPreco is null)
                return NotFound();
                        
            if (await _produtoPrecoService.DeleteAsync(existingProdutoPreco) == false)
                return NotFound();

            return NoContent();
        }
    }
}
