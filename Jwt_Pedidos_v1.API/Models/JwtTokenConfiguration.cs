using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jwt_Pedidos_v1.API.Models
{
    public sealed class JwtTokenConfiguration
    {
        public string Key { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ExpirationInSeconds { get; set; }
    }
}
