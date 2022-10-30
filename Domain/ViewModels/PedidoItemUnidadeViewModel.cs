using System.Text.Json.Serialization;

namespace Domain.ViewModels
{
	public class PedidoItemUnidadeViewModel
    {
        [JsonPropertyName("ItemId")]
        public int UnidadeId { get; set; }

        [JsonPropertyName("Sigla")]
        public string Nome { get; set; }  
    }
}
