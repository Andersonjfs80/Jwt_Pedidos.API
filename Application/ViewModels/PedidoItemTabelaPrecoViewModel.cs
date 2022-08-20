using System.Text.Json.Serialization;

namespace Application.ViewModels
{
	public class PedidoItemTabelaPrecoViewModel
    {
        [JsonPropertyName("ItemId")]
        public int TabelaPrecoId { get; set; }

        public string Nome { get; set; }        
    }
}
