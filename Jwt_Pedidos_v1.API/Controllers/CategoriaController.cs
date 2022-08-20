using Application.Interfaces.Domain;
using Domain.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Jwt_Pedidos_v1.API.Controllers
{
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet] 
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<IEnumerable<Categoria>> GetAll()
        {
            var categorias = _categoriaService.GetAllAsync();
            
            return Ok(JsonSerializer.Serialize(categorias, new JsonSerializerOptions()
            {
                MaxDepth = 0,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                IgnoreReadOnlyProperties = true
            }));
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _categoriaService.GetByIdAsync(c => c.CategoriaId == id).Result;

            if (categoria is null)
                return NotFound();
            
            return Ok(JsonSerializer.Serialize(categoria, new JsonSerializerOptions()
            {
                MaxDepth = 0,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                IgnoreReadOnlyProperties = true
            }));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Create(Categoria categoria)
        {
            if (_categoriaService.AddAsync(categoria).Result == false)
                return NotFound();

            return CreatedAtAction(nameof(Create), categoria);
        }
 
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Update(int id, Categoria categoria)
        {
            if (id <= 0)
                return NotFound();

            if (categoria is null)
                return NotFound();

            if (categoria.CategoriaId != id)
                return NotFound();

            if (_categoriaService.Update(categoria).Result == false)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
