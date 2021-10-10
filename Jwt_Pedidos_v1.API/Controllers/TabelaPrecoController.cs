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
using System.Text.Json;
using System.Threading.Tasks;

namespace Jwt_Pedidos_v1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TabelaPrecoController : ControllerBase
    {
        private readonly ITabelaPrecoService _tabelaPrecoService;
        public TabelaPrecoController(ITabelaPrecoService tabelaPrecoService)
        {
            _tabelaPrecoService = tabelaPrecoService;
        }

        [HttpGet]        
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<IEnumerable<TabelaPreco>> GetAll()
        {
            var tabelaPrecos = _tabelaPrecoService.GetAllAsync();
            
            return Ok(JsonSerializer.Serialize(tabelaPrecos, new JsonSerializerOptions()
            {
                MaxDepth = 0,
                IgnoreNullValues = true,
                IgnoreReadOnlyProperties = true
            }));
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<TabelaPreco> Get(int id)
        {
            var tabelaPreco = _tabelaPrecoService.GetByIdAsync(tp => tp.TabelaPrecoId == id).Result;

            if (tabelaPreco is null)
                return NotFound();
            
            return Ok(JsonSerializer.Serialize(tabelaPreco, new JsonSerializerOptions()
            {
                MaxDepth = 0,
                IgnoreNullValues = true,
                IgnoreReadOnlyProperties = true
            }));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Create(TabelaPreco tabelaPreco)
        {
            if (_tabelaPrecoService.AddAsync(tabelaPreco).Result == false)
                return NotFound();

            return CreatedAtAction(nameof(Create), tabelaPreco);
        }
 
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Update(int id, TabelaPreco tabelaPreco)
        {
            if (id <= 0)
                return NotFound();

            if (tabelaPreco is null)
                return NotFound();

            if (tabelaPreco.TabelaPrecoId != id)
                return NotFound();

            if (_tabelaPrecoService.Update(tabelaPreco).Result == false)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Delete(int id)
        {
            var existingTabelaPreco = _tabelaPrecoService.GetByIdAsync(tp => tp.TabelaPrecoId == id).Result;

            if (existingTabelaPreco is null)
                return NotFound();

            if (_tabelaPrecoService.Delete(existingTabelaPreco).Result == false)
                return NotFound();

            return NoContent();
        }
    }
}
