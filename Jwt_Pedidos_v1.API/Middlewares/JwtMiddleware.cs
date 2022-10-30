using Jwt_Pedidos_v1.API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwt_Pedidos_v1.API.Middlewares
{
    public static class JwtMiddleware
    {
        public static void AddJwtMiddleware(this IServiceCollection services, IConfiguration configuration)
        {
            UseJwtMiddleware(services, configuration);
            UseJwtAuthenticationMiddlerware(services, configuration);
        }

        private static void UseJwtMiddleware(IServiceCollection services, IConfiguration configuration)
        {
            var tokenConfiguration = configuration.GetSection("JwtTokenConfiguration").Get<JwtTokenConfiguration>();
            
            services.AddSingleton(tokenConfiguration);
        }

        private static void UseJwtAuthenticationMiddlerware(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {                   
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtTokenConfiguration:Issuer"],
                    ValidAudience = configuration["JwtTokenConfiguration:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JwtTokenConfiguration:Key"])),
                    ClockSkew = TimeSpan.Zero
            };               
            });
        }

    }
}
