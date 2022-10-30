using System.Text.Json.Serialization;

namespace Domain.ViewModels
{
	public class PedidoItemTabelaPrecoViewModel
    {
        [JsonPropertyName("ItemId")]
        public int TabelaPrecoId { get; set; }

        public string Nome { get; set; }        
    }
}
