using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Jwt_Pedidos_v1.API.Middlewares
{
    public static class SwaggerMiddlerware
    {
        public static void AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Jwt_Pedidos_v1.API", Version = "v1" });
                opt.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Description = "Autenticação baseada em Json Web Token (JWT)",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "bearer"
                            },
                            Type = SecuritySchemeType.Http,
                            Scheme = "bearer",
                            BearerFormat = "JWT"
                        },
                        new List<string>()
                    }
                });
            });
        }

        public static void AddSwaggerApp(this IApplicationBuilder app, string routePrefix)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Anderson Auhorization Jwt Api");

                c.RoutePrefix = routePrefix;
            });
        }
    }
}
