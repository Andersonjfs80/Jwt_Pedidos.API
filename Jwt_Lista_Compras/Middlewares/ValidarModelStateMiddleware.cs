using Jwt_Pedidos_v1.API.Middlewares.Attributes;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Jwt_Pedidos_v1.API.Middlewares
{
	public class ValidarModelStateMiddleware 
	{
		//private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

		private RequestDelegate _next;

		public ValidarModelStateMiddleware(RequestDelegate next)
		{
			//_logger = logger;
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			var model = GetModelView(context);
			if (model != null)
			{
				var validationResult = ValidationErrors(model);
				if (validationResult != null)
				{
					context.Response.ContentType = "application/text";
					context.Response.StatusCode = (int)HttpStatusCode.NotFound;

					await context.Response.WriteAsync(JsonConvert.SerializeObject(validationResult));
					return;
				}
			}

			await _next(context);
		}

		private async Task<object> GetModelView(HttpContext context)
		{
			var attribute = context.Request.HttpContext.GetEndpoint().Metadata.GetMetadata<ValidarModelStateAttribute>();

			context.Request.EnableBuffering();
			context.Request.Body.Position = 0;

			var streamReader = new StreamReader(context.Request.Body);
			var bodyAsText = await streamReader.ReadToEndAsync();
			dynamic json = JsonConvert.DeserializeObject(bodyAsText, attribute.TypeModel);

			return json;	
		}

		public IEnumerable<ValidationResult> ValidationErrors(object obj)
		{
			var contexto = new ValidationContext(obj, null, null);
			var resultadoValidacao = new List<ValidationResult>();			
			Validator.TryValidateObject(obj, contexto, resultadoValidacao, true);

			return resultadoValidacao;
		}
	}
}
