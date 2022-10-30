using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels.CustomExceptions
{
	public class ErrorMessageException : Exception
	{
		public HttpStatusCode StatusCode { get; set; }

		public string Mensagem { get; set; }
	}
}
