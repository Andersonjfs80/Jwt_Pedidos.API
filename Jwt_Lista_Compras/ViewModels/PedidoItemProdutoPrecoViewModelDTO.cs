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
    public class PedidoItemProdutoPrecoViewModelDTO
    {
        [JsonPropertyName("ItemId")]
        public int ProdutoPrecoId { get; set; }

        public double PrecoCusto { get; set; }

        public double PrecoVenda { get; set; }

        public double QuantidadeConversao { get; set; }
    }
}
