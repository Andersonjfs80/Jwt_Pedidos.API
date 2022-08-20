using Jwt_Lista_Compras.Middlewares;
using Jwt_Pedidos_v1.API.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;
using System.Linq;

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

            //services.AddControllersWithViews()
            //                      //.AddJsonOptions(o => o.JsonSerializerOptions
            //                      //.ReferenceHandler = ReferenceHandler.Preserve)
            //                      .ConfigureApiBehaviorOptions(options =>
            //                      {
            //                          options.SuppressMapClientErrors = true;
            //                      });

            //Versioning API
            services.AddApiVersioning();
            services.AddCors();
            services.AddControllers();
            
            //Custom
            LoadCustomMiddlers(services, Configuration);
           // services.AddGlobalExceptionHandlerMiddleware();

            //services.AddControllers(options =>
            //{
            //	//adicionado por instância 
            //	options.Filters.Add(new GlobalExceptionHandlerMiddleware());
            //	//adicionado por tipo  
            //	//options.Filters.Add(typeof(CustomActionFilter));
            //});

            //services.Configure<ApiBehaviorOptions>(options =>
            //{
            //	//options.SuppressMapClientErrors = false;
            ////	options.SuppressModelStateInvalidFilter = true;

            ////	 options.SuppressConsumesConstraintForFormFileParameters = true;
            //});

            //         services.Configure<ApiBehaviorOptions>(options =>
            //         {

            //	//options.InvalidModelStateResponseFactory = actionContext =>
            //	//{
            //	//	if (actionContext.HttpContext.GetEndpoint().Metadata.GetMetadata<GlobalExceptionHandlerMiddleware>() != null)
            //	//	{
            //	//		var errors = actionContext.ModelState.First().Value.Errors.First().ErrorMessage;

            //	//		return new BadRequestObjectResult(errors);
            //	//	}
            //	//	else
            //	//	{


            // //                    return new BadRequestObjectResult(actionContext.HttpContext.GetEndpoint().Metadata.GetMetadata<ControllerContext>().ModelState);
            //	//	}

            //	//	//var errors = actionContext
            //	//	//  .ModelState.First()
            //	//	//  .Value.Errors.First()
            //	//	//  .ErrorMessage.ToArray();

            //	//	//return new BadRequestObjectResult(errors);
            //	//};
            //});
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

            //app.UseWhen(context => context.Request.HttpContext.GetEndpoint().Metadata.GetMetadata<GlobalExceptionHandlerMiddleware>() != null,            
            //    appBuilder =>            
            //    {
            //        app.UseGlobalExceptionHandlerMiddleware();
            //});

            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            //app.UseGlobalExceptionHandlerMiddleware();

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
