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
    public class ProdutoCategoriaController : ControllerBase
    {
        private readonly IProdutoCategoriaService _produtoCategoriaService;
        public ProdutoCategoriaController(IProdutoCategoriaService produtoCategoriaService)
        {
            _produtoCategoriaService = produtoCategoriaService;            
        }

		[HttpGet]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> GetAll() => Ok(await _produtoCategoriaService.GetAllAsync());

		[HttpGet("{id}")]
		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		public async Task<IActionResult> Get(int id) => Ok(await _produtoCategoriaService.GetByIdAsync(pc => pc.ProdutoCategoriaId == id));
        //var produtoCategoria = _produtoCategoriaService.GetByIdAsync(pc => pc.ProdutoCategoriaId == id).Result;

        //if (produtoCategoria is null)
        //    return NotFound();

        //JsonSerializerOptions options = new()
        //{
        //    IgnoreReadOnlyProperties = true,
        //    WriteIndented = true,
        //    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
        //};



        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Create(ProdutoCategoria produtoCategoria)
        {           
            if (!await _produtoCategoriaService.AddAsync(produtoCategoria) == false)
                return NotFound();

            return CreatedAtAction(nameof(Create), produtoCategoria);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(int id, ProdutoCategoria produtoCategoria)
        {
            if (id <= 0)
                return NotFound();

            if (produtoCategoria is null)
                return NotFound();

            if (produtoCategoria.ProdutoCategoriaId != id)
                return NotFound();
                        
            if (!await _produtoCategoriaService.UpdateAsync(produtoCategoria) == false)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(int id)
        {
            var existingProdutoCategoria = _produtoCategoriaService.GetByIdAsync(pc => pc.ProdutoCategoriaId == id).Result;

            if (existingProdutoCategoria is null)
                return NotFound();
                                    
            if (!await _produtoCategoriaService.DeleteAsync(existingProdutoCategoria) == false)
                return NotFound();

            return NoContent();
        }
    }
}
