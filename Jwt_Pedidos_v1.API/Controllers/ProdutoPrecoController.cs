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
    public class ProdutoPrecoController : ControllerBase
    {
        private readonly IProdutoPrecoService _produtoPrecoService;
        public ProdutoPrecoController(IProdutoPrecoService produtoPrecoService)
        {
            _produtoPrecoService = produtoPrecoService;            
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<IEnumerable<ProdutoPreco>> GetAll()
        {
            var produtosCategorias = _produtoPrecoService.GetAllAsync();

            return Ok(JsonSerializer.Serialize(produtosCategorias, new JsonSerializerOptions()
            {
                MaxDepth = 0,
                IgnoreNullValues = true,
                IgnoreReadOnlyProperties = true
            }));
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<ProdutoPreco> Get(int id)
        {
            var produtoPreco = _produtoPrecoService.GetByIdAsync(pc => pc.ProdutoPrecoId == id).Result;

            if (produtoPreco is null)
                return NotFound();

            return Ok(JsonSerializer.Serialize(produtoPreco, new JsonSerializerOptions()
            {
                MaxDepth = 0,
                IgnoreNullValues = true,
                IgnoreReadOnlyProperties = true
            }));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Create(ProdutoPreco produtoPreco)
        {
            if (_produtoPrecoService.AddAsync(produtoPreco).Result == false)
                return NotFound();

            return CreatedAtAction(nameof(Create), produtoPreco);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Update(int id, ProdutoPreco produtoPreco)
        {
            if (id <= 0)
                return NotFound();

            if (produtoPreco is null)
                return NotFound();

            if (produtoPreco.ProdutoPrecoId != id)
                return NotFound();

            if (_produtoPrecoService.Update(produtoPreco).Result == false)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Delete(int id)
        {
            var existingProdutoPreco = _produtoPrecoService.GetByIdAsync(pc => pc.ProdutoPrecoId == id).Result;

            if (existingProdutoPreco is null)
                return NotFound();

            if (_produtoPrecoService.Delete(existingProdutoPreco).Result == false)
                return NotFound();

            return NoContent();
        }
    }
}
