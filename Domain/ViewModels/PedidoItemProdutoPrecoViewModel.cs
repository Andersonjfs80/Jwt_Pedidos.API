using System.Text.Json.Serialization;

namespace Domain.ViewModels
{
	public class PedidoItemProdutoPrecoViewModel
    {
        [JsonPropertyName("ItemId")]
        public int ProdutoPrecoId { get; set; }

        public double PrecoCusto { get; set; }

        public double PrecoVenda { get; set; }

        public double QuantidadeConversao { get; set; }
    }
}
