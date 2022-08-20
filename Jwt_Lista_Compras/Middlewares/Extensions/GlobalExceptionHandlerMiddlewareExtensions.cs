using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jwt_Pedidos_v1.API.Middlewares.Extensions
{
	public static class GlobalExceptionHandlerMiddlewareExtensions
	{
		public static IServiceCollection AddGlobalExceptionHandlerMiddleware(this IServiceCollection services)
		{
			return services.AddScoped<GlobalExceptionHandlerMiddleware>();
		}

		public static void UseGlobalExceptionHandlerMiddleware(this IApplicationBuilder app)
		{
			app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
		}
	}
}
