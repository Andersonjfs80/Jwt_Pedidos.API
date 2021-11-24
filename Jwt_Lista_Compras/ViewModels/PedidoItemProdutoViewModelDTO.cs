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
    public class PedidoItemProdutoViewModelDTO
    {
        public PedidoItemProdutoViewModelDTO()
        {
            ProdutoPreco = new PedidoItemProdutoPrecoViewModelDTO();
            TabelaPreco = new PedidoItemTabelaPrecoViewModelDTO();
            Unidade = new PedidoItemUnidadeViewModelDTO();
        }
        //[JsonPropertyName("ItemId")]
        //public int ProdutoCodigoId { get; set; }

        [JsonPropertyName("ItemId")]
        public int ProdutoId { get; set; }        

        public string Codigo { get; set; }

        public string Nome { get; set; }

        [JsonPropertyName("Preco")]
        public virtual PedidoItemProdutoPrecoViewModelDTO ProdutoPreco { get; set; }

        [JsonPropertyName("TabelaPreco")]
        public virtual PedidoItemTabelaPrecoViewModelDTO TabelaPreco { get; set; }

        [JsonPropertyName("Unidade")]
        public virtual PedidoItemUnidadeViewModelDTO Unidade { get; set; }
    }
}
