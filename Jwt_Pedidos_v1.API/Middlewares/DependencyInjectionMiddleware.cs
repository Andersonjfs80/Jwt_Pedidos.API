using Application.Interfaces.Domain;
using Application.Service.Domain;
using Infrastructure.Interfaces.Domain;
using Infrastructure.Repositories.Domain;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jwt_Pedidos_v1.API.Middlewares
{
    public static class DependencyInjectionMiddleware
    {
        public static void AddDependencyInjectionMiddleware(this IServiceCollection services)
        {
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<ICategoriaService, CategoriaService>();
        }
    }
}

