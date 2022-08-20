using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
	public class ErroMensagemException : Exception
	{
		public HttpStatusCode StatusCode { get; set; }

		public string Mensagem { get; set; }
	}
}
