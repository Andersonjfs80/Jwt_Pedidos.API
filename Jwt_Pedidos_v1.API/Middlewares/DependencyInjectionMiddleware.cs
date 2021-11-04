using Application.Interfaces.Domain;
using Application.Service.Domain;
using Infrastructure.DBConfiguration;
using Infrastructure.DBConfiguration.EFCore;
using Infrastructure.Interfaces.Domain;
using Infrastructure.Repositories.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
            IConfiguration dbConnectionSettings = DatabaseConnection.ConnectionConfiguration;
            string conn = dbConnectionSettings.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(conn));

            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<ICategoriaService, CategoriaService>();
            services.AddTransient<ICategoriaItemRepository, CategoriaItemRepository>();
            services.AddTransient<ICategoriaItemService, CategoriaItemService>();
            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddTransient<IProdutoService, ProdutoService>();
            services.AddTransient<IProdutoCategoriaRepository, ProdutoCategoriaRepository>();
            services.AddTransient<IProdutoCategoriaService, ProdutoCategoriaService>();
            services.AddTransient<IProdutoPrecoRepository, ProdutoPrecoRepository>();
            services.AddTransient<IProdutoPrecoService, ProdutoPrecoService>();
            services.AddTransient<IUnidadeRepository, UnidadeRepository>();
            services.AddTransient<IUnidadeService, UnidadeService>();
            services.AddTransient<ITabelaPrecoRepository, TabelaPrecoRepository>();
            services.AddTransient<ITabelaPrecoService, TabelaPrecoService>();
            services.AddTransient<IPedidoService, PedidoService>();
            services.AddTransient<IPedidoRepository, PedidoRepository>();
            services.AddTransient<IPedidoItemService, PedidoItemService>();
            services.AddTransient<IPedidoItemRepository, PedidoItemRepository>();
        }
    }
}

