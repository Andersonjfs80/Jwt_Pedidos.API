using Domain.Entidades;
using Jwt_Pedidos_v1.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jwt_Pedidos_v1.API.Services;

namespace Jwt_Pedidos_v1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private JwtTokenConfiguration _tokenConfiguration;
        public AuthenticationController(JwtTokenConfiguration tokenConfiguration)
        {
            _tokenConfiguration = tokenConfiguration;
        }

        [HttpPost]
        public IActionResult Login(UsuarioDTO usuario)
        {
            bool resultado = ValidateUser(usuario);
            if (resultado)
            {
                var tokenString = TokenService.CreateTokenJWT(_tokenConfiguration);
                return Ok(new { token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }

        private bool ValidateUser(UsuarioDTO usuario)
        {
            if (usuario.Nome == "Anderson" && usuario.Senha == "NS123456")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
