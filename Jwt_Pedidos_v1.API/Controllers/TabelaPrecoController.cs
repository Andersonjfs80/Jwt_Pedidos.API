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
    public class TabelaPrecoController : ControllerBase
    {
        private readonly ITabelaPrecoService _tabelaPrecoService;
        public TabelaPrecoController(ITabelaPrecoService tabelaPrecoService)
        {
            _tabelaPrecoService = tabelaPrecoService;
        }

        [HttpGet]        
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _tabelaPrecoService.GetAllAsync());
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _tabelaPrecoService.GetByIdAsync(tp => tp.TabelaPrecoId == id));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Create(TabelaPreco tabelaPreco)
        {
            if (!await _tabelaPrecoService.AddAsync(tabelaPreco) == false)
                return NotFound();

            return CreatedAtAction(nameof(Create), tabelaPreco);
        }
 
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(int id, TabelaPreco tabelaPreco)
        {
            if (id <= 0)
                return NotFound();

            if (tabelaPreco is null)
                return NotFound();

            if (tabelaPreco.TabelaPrecoId != id)
                return NotFound();
                        
            if (!await _tabelaPrecoService.UpdateAsync(tabelaPreco)  == false)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(int id)
        {
            var existingTabelaPreco = await _tabelaPrecoService.GetByIdAsync(tp => tp.TabelaPrecoId == id);

            if (existingTabelaPreco is null)
                return NotFound();
                        
            if (!await _tabelaPrecoService.DeleteAsync(existingTabelaPreco) == false)
                return NotFound();

            return NoContent();
        }
    }
}
