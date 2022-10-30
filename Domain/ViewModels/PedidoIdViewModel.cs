using Domain.ValidationAttributes;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.ViewModels
{
	public class PedidoIdViewModel
    {        
        [JsonPropertyName("pedidoId")]
        public int PedidoId { get; set; }
    }
}
