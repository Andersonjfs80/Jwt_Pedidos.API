using System.Text.Json.Serialization;

namespace Application.ViewModels
{
	public class PedidoItemProdutoViewModel
    {
        public PedidoItemProdutoViewModel()
        {
            ProdutoPreco = new PedidoItemProdutoPrecoViewModel();
            TabelaPreco = new PedidoItemTabelaPrecoViewModel();
            Unidade = new PedidoItemUnidadeViewModel();
        }
        //[JsonPropertyName("ItemId")]
        //public int ProdutoCodigoId { get; set; }

        [JsonPropertyName("ItemId")]
        public int ProdutoId { get; set; }        

        public string Codigo { get; set; }

        public string Nome { get; set; }

        [JsonPropertyName("Preco")]
        public virtual PedidoItemProdutoPrecoViewModel ProdutoPreco { get; set; }

        [JsonPropertyName("TabelaPreco")]
        public virtual PedidoItemTabelaPrecoViewModel TabelaPreco { get; set; }

        [JsonPropertyName("Unidade")]
        public virtual PedidoItemUnidadeViewModel Unidade { get; set; }
    }
}
