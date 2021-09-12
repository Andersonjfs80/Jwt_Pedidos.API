using Jwt_Pedidos_v1.API.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jwt_Pedidos_v1.API.Middlewares
{
    public static class JwtMiddleware
    {
        public static void AddJwtMiddleware(this IServiceCollection services, IConfiguration configuration)
        {
            UseJwtMiddleware(services, configuration);
        }

        private static void UseJwtMiddleware(IServiceCollection services, IConfiguration configuration)
        {
            var tokenConfiguration =
              configuration.GetSection("JwtTokenConfiguration").Get<JwtTokenConfiguration>();

            services.AddSingleton<JwtTokenConfiguration>(tokenConfiguration);
        }

    }
}
