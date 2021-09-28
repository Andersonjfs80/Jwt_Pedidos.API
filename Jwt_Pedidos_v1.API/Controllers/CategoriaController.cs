using Application.Interfaces.Domain;
using Domain.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        //[Authorize]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<IEnumerable<Categoria>> GetAll()
        {
            var categorias = _categoriaService.GetAllAsync();
            
            return Ok(JsonSerializer.Serialize(categorias, GetConfigurationJsonSerializerOptions()));
        }

        private static JsonSerializerOptions GetConfigurationJsonSerializerOptions()
        {
            return new JsonSerializerOptions()
            {
                MaxDepth = 0,
                IgnoreNullValues = true,
                IgnoreReadOnlyProperties = true
            };
        }

        [HttpGet("{id}")]
        //[Authorize]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _categoriaService.GetByIdAsync(c => c.CategoriaId == id);

            if (categoria is null)
                return NotFound();

            return Ok(JsonSerializer.Serialize(categoria, GetConfigurationJsonSerializerOptions()));
        }

        [HttpPost]
        //[Authorize]
        public IActionResult Create(Categoria categoria)
        {            
            if (_categoriaService.AddAsync(categoria).Result == false)
                return NotFound();

            return CreatedAtAction(nameof(Create), categoria);
        }
 
        [HttpPut]
        //[HttpPut("{id}")]
        //[Authorize]
        public IActionResult Update(Categoria categoria)
        {
            if (categoria is null)
                return NotFound();

            if (categoria.CategoriaId <= 0)
                return NotFound();

            if (_categoriaService.Update(categoria).Result == false)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        //[Authorize]
        public IActionResult Delete(int id)
        {
            var existingCategoria = _categoriaService.GetByIdAsync(c => c.CategoriaId == id).Result;

            if (existingCategoria is null)
                return NotFound();

            if (_categoriaService.Delete(existingCategoria).Result == false)
                return NotFound();

            return NoContent();
        }
    }
}
