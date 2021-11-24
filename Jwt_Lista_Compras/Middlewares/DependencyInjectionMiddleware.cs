using Application.Interfaces.Domain;
using Application.Service.Domain;
using Infrastructure.DBConfiguration;
using Infrastructure.DBConfiguration.EFCore;
using Infrastructure.Interfaces.Domain;
using Infrastructure.Repositories.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jwt_Lista_Compras.Middlewares
{
    public static class DependencyInjectionMiddleware
    {
        public static void AddDependencyInjectionMiddleware(this IServiceCollection services)
        {
            IConfiguration dbConnectionSettings = DatabaseConnection.ConnectionConfiguration;
            string conn = dbConnectionSettings.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(conn));

            //Services
            services.AddTransient<IPedidoService, PedidoService>();
            services.AddTransient<IPedidoItemService, PedidoItemService>();
            //Repositories
            services.AddTransient<IPedidoRepository, PedidoRepository>();
            services.AddTransient<IPedidoItemRepository, PedidoItemRepository>();
        }
    }
}

