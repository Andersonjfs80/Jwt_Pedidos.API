using Jwt_Lista_Compras.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Jwt_Lista_Compras
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc().AddNewtonsoftJson(
            //  options => options.SerializerSettings.ContractResolver = new DefaultContractResolver()

            //).AddNewtonsoftJson(
            //  options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize

            //); //Caso não funcionar em referencia circular adiconar o atributi [JsonIgnore]

            services.AddControllersWithViews()
                                  //.AddJsonOptions(o => o.JsonSerializerOptions
                                  //.ReferenceHandler = ReferenceHandler.Preserve)
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
            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
            });
        }
    }
}
