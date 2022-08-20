using Application.Common;
using Application.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Jwt_Pedidos_v1.API.Middlewares
{
	public class CamposValidacao
	{
		[ValidarCPFCNPJ(ErrorMessage = "teste invalido")]		
		public int cpf { get; set; }
	}
		
	public class GlobalExceptionHandlerMiddleware 
	{
		//private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

		private RequestDelegate _next;

		public GlobalExceptionHandlerMiddleware(RequestDelegate next)
		{
			//_logger = logger;
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				//_logger.LogError($"Erro interno no servidor. Mensagem original: {ex}");
				await HandleExceptionAsync(context, ex);
			}
		}
		
		private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = MediaTypeNames.Application.Json;

			switch (exception)
			{
				case ErroMensagemException ex:
					
					context.Response.StatusCode = (int)ex.StatusCode;
					await context.Response.WriteAsync(ex.Mensagem);

					break;
				case NaoEncontradoException ex:

					context.Response.StatusCode = (int)ex.StatusCode;
					await context.Response.WriteAsync(ex.Mensagem);

					break;
				default:
					context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

					var json = new
					{
						Title = "Erro interno no servidor.",
						StatusCode = context.Response.StatusCode,
						Message = $"Erro interno no servidor. Mensagem original: {exception}.",
						Error = exception
					};

					await context.Response.WriteAsync(JsonConvert.SerializeObject(json));
					break;
			}
		}
	}
}
