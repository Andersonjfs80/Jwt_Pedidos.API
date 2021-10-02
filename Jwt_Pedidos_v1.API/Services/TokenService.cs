using Jwt_Pedidos_v1.API.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwt_Pedidos_v1.API.Services
{
    public static class TokenService
    {
        public static string CreateTokenJWT(JwtTokenConfiguration tokenConfiguration)
        {
            var token = new JwtSecurityToken(
                issuer: tokenConfiguration.Issuer,
                audience: tokenConfiguration.Audience,
                expires: DateTime.UtcNow.AddSeconds(tokenConfiguration.ExpirationInSeconds),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfiguration.Key)),
                    SecurityAlgorithms.HmacSha256));

            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }

    }
}
