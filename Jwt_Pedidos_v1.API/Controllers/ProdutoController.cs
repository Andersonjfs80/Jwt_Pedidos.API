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
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _produtoService.GetAllAsync());
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _produtoService.GetByIdAsync(p => p.ProdutoId == id));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Create(Produto produto)
        {            
            if (!await _produtoService.AddAsync(produto) == false)
                return NotFound();

            return CreatedAtAction(nameof(Create), produto);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(int id, Produto produto)
        {
            if (id <= 0)
                return NotFound();

            if (produto is null)
                return NotFound();

            if (produto.ProdutoId != id)
                return NotFound();
                        
            if (!await _produtoService.UpdateAsync(produto) == false)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(int id)
        {
            var existingProduto = await _produtoService.GetByIdAsync(p => p.ProdutoId == id);

            if (existingProduto is null)
                return NotFound();
                        
            if (!await _produtoService.DeleteAsync(existingProduto) == false)
                return NotFound();

            return NoContent();
        }
    }
}
