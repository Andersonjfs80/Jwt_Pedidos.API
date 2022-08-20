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
        public ActionResult<IEnumerable<Produto>> GetAll()
        {
            var produtos = _produtoService.GetAllAsync();

            return Ok(JsonSerializer.Serialize(produtos, new JsonSerializerOptions()
            {
                MaxDepth = 0,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                IgnoreReadOnlyProperties = true
            }));
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _produtoService.GetByIdAsync(p => p.ProdutoId == id).Result;

            if (produto is null)
                return NotFound();

            return Ok(JsonSerializer.Serialize(produto, new JsonSerializerOptions()
            {
                MaxDepth = 0,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                IgnoreReadOnlyProperties = true
            }));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Create(Produto produto)
        {
            if (_produtoService.AddAsync(produto).Result == false)
                return NotFound();

            return CreatedAtAction(nameof(Create), produto);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Update(int id, Produto produto)
        {
            if (id <= 0)
                return NotFound();

            if (produto is null)
                return NotFound();

            if (produto.ProdutoId != id)
                return NotFound();

            if (_produtoService.Update(produto).Result == false)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Delete(int id)
        {
            var existingProduto = _produtoService.GetByIdAsync(p => p.ProdutoId == id).Result;

            if (existingProduto is null)
                return NotFound();

            if (_produtoService.Delete(existingProduto).Result == false)
                return NotFound();

            return NoContent();
        }
    }
}
