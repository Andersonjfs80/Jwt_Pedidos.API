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
    public class ProdutoCategoriaController : ControllerBase
    {
        private readonly IProdutoCategoriaService _produtoCategoriaService;
        public ProdutoCategoriaController(IProdutoCategoriaService produtoCategoriaService)
        {
            _produtoCategoriaService = produtoCategoriaService;            
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<IEnumerable<ProdutoCategoria>> GetAll()
        {
            var produtosCategorias = _produtoCategoriaService.GetAllAsync();

            return Ok(JsonSerializer.Serialize(produtosCategorias, new JsonSerializerOptions()
            {
                MaxDepth = 0,
                IgnoreNullValues = true,
                IgnoreReadOnlyProperties = true
            }));
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<ProdutoCategoria> Get(int id)
        {
            var produtoCategoria = _produtoCategoriaService.GetByIdAsync(pc => pc.ProdutoCategoriaId == id).Result;

            if (produtoCategoria is null)
                return NotFound();

            return Ok(JsonSerializer.Serialize(produtoCategoria, new JsonSerializerOptions()
            {
                MaxDepth = 0,
                IgnoreNullValues = true,
                IgnoreReadOnlyProperties = true
            }));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Create(ProdutoCategoria produtoCategoria)
        {
            if (_produtoCategoriaService.AddAsync(produtoCategoria).Result == false)
                return NotFound();

            return CreatedAtAction(nameof(Create), produtoCategoria);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Update(int id, ProdutoCategoria produtoCategoria)
        {
            if (id <= 0)
                return NotFound();

            if (produtoCategoria is null)
                return NotFound();

            if (produtoCategoria.ProdutoCategoriaId != id)
                return NotFound();

            if (_produtoCategoriaService.Update(produtoCategoria).Result == false)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Delete(int id)
        {
            var existingProdutoCategoria = _produtoCategoriaService.GetByIdAsync(pc => pc.ProdutoCategoriaId == id).Result;

            if (existingProdutoCategoria is null)
                return NotFound();

            if (_produtoCategoriaService.Delete(existingProdutoCategoria).Result == false)
                return NotFound();

            return NoContent();
        }
    }
}
