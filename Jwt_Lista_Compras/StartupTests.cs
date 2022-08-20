using Jwt_Lista_Compras.Middlewares;
using Jwt_Pedidos_v1.API.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;

namespace Jwt_Lista_Compras
{
	public class StartupTests
    {
        public IConfiguration Configuration { get; }
        public StartupTests()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path: "appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IConfiguration>(Configuration);
            ConfigureServices(serviceCollection);
        }        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        { 
            services.AddControllersWithViews()
                                  .ConfigureApiBehaviorOptions(options =>
                                  {
                                      options.SuppressMapClientErrors = true;
                                  });

            services.AddCors();
            services.AddControllers();            
            //Custom
            LoadCustomMiddlers(services, Configuration);
        }

        private static void LoadCustomMiddlers(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddJwtMiddleware(configuration);
            //services.AddLoggerMiddleware();
            services.AddDependencyInjectionMiddleware();
            services.AddSwaggerService();            
            services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.AddSwaggerApp();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
