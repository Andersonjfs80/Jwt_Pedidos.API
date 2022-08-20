using Application.Interfaces.Domain;
using Domain.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class UnidadeController : ControllerBase
    {
        private readonly IUnidadeService _unidadeService;
        public UnidadeController(IUnidadeService unidadeService)
        {
            _unidadeService = unidadeService;
        }

        [HttpGet]        
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<IEnumerable<Unidade>> GetAll()
        {
            var unidades = _unidadeService.GetAllAsync();
            
            return Ok(JsonSerializer.Serialize(unidades, new JsonSerializerOptions()
            {
                MaxDepth = 0,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                IgnoreReadOnlyProperties = true
            }));
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<Unidade> Get(int id)
        {
            var unidade = _unidadeService.GetByIdAsync(u => u.UnidadeId == id).Result;

            if (unidade is null)
                return NotFound();
            
            return Ok(JsonSerializer.Serialize(unidade, new JsonSerializerOptions()
            {
                MaxDepth = 0,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                IgnoreReadOnlyProperties = true
            }));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Create(Unidade unidade)
        {
            if (_unidadeService.AddAsync(unidade).Result == false)
                return NotFound();

            return CreatedAtAction(nameof(Create), unidade);
        }
 
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Update(int id, Unidade unidade)
        {
            if (id <= 0)
                return NotFound();

            if (unidade is null)
                return NotFound();

            if (unidade.UnidadeId != id)
                return NotFound();

            if (_unidadeService.Update(unidade).Result == false)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Delete(int id)
        {
            var existingUnidade = _unidadeService.GetByIdAsync(u => u.UnidadeId == id).Result;

            if (existingUnidade is null)
                return NotFound();

            if (_unidadeService.Delete(existingUnidade).Result == false)
                return NotFound();

            return NoContent();
        }
    }
}
