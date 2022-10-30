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
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _unidadeService.GetAllAsync());
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _unidadeService.GetByIdAsync(u => u.UnidadeId == id));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Create(Unidade unidade)
        {            
            if (!await _unidadeService.AddAsync(unidade) == false)
                return NotFound();

            return CreatedAtAction(nameof(Create), unidade);
        }
 
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(int id, Unidade unidade)
        {
            if (id <= 0)
                return NotFound();

            if (unidade is null)
                return NotFound();

            if (unidade.UnidadeId != id)
                return NotFound();
                        
            if (!await _unidadeService.UpdateAsync(unidade) == false)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(int id)
        {
            var existingUnidade = await _unidadeService.GetByIdAsync(u => u.UnidadeId == id);

            if (existingUnidade is null)
                return NotFound();
                        
            if (!await _unidadeService.DeleteAsync(existingUnidade) == false)
                return NotFound();

            return NoContent();
        }
    }
}
