using Application.Interfaces.Domain;
using Domain.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Jwt_Pedidos_v1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class CategoriaItemController : ControllerBase
    {
        private readonly ICategoriaItemService _categoriaItemService;
        public CategoriaItemController(ICategoriaItemService categoriaItemService)
        {
            _categoriaItemService = categoriaItemService;
        }

        // GET: api/<CategoriaItemController>
        [HttpGet]        
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]        
        public ActionResult<IEnumerable<CategoriaItem>> GetAll()
        {
            var categoriaItens = _categoriaItemService.GetAllAsync();

            return Ok(JsonSerializer.Serialize(categoriaItens, new JsonSerializerOptions()
            {
                MaxDepth = 0,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                IgnoreReadOnlyProperties = true
            }));
        }

        // GET api/<CategoriaItemController>/5
        [HttpGet("{id}")]        
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CategoriaItem> Get(int id)
        {
            var categoriaItem = _categoriaItemService.GetByIdAsync(c => c.CategoriaItemId == id).Result;

            if (categoriaItem is null)
                return NotFound();

            return Ok(JsonSerializer.Serialize(categoriaItem, new JsonSerializerOptions()
            {
                MaxDepth = 0,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                IgnoreReadOnlyProperties = true
            }));
        }

        // POST api/<CategoriaItemController>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Create([FromBody] CategoriaItem categoriaItem)
        {
            if (_categoriaItemService.AddAsync(categoriaItem).Result == false)
                return NotFound();

            return CreatedAtAction(nameof(Create), categoriaItem);
        }

        // PUT api/<CategoriaItemController>/5
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, [FromBody] CategoriaItem categoriaItem)
        {
            if (id <= 0)
                return NotFound();

            if (categoriaItem is null)
                return NotFound();

            if (categoriaItem.CategoriaItemId != id)
                return NotFound();

            if (_categoriaItemService.Update(categoriaItem).Result == false)
                return NotFound();

            return NoContent();
        }

        // DELETE api/<CategoriaItemController>/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var existingCategoriaItem = _categoriaItemService.GetByIdAsync(c => c.CategoriaItemId == id).Result;

            if (existingCategoriaItem is null)
                return NotFound();

            if (_categoriaItemService.Delete(existingCategoriaItem).Result == false)
                return NotFound();

            return NoContent();
        }
    }
}
