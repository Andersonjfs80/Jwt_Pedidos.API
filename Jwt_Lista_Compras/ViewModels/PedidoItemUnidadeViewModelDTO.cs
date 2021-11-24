using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Jwt_Pedidos_v1.API.ViewModels
{
    public class PedidoItemUnidadeViewModelDTO
    {
        [JsonPropertyName("ItemId")]
        public int UnidadeId { get; set; }

        [JsonPropertyName("Sigla")]
        public string Nome { get; set; }  
    }
}
