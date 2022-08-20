using Domain.Entidades;
using System.Text.Json.Serialization;

namespace Application.ViewModels
{
	public class PedidoItemViewModel
    {
        public PedidoItemViewModel()
        {
            Produto = new PedidoItemProdutoViewModel();
        }

        [JsonPropertyName("itemId")]
        public int PedidoItemId { get; set; }

        [JsonPropertyName("pedidoId")]
        public int PedidoId { get; set; }

        [JsonIgnore]
        public virtual Pedido Pedido { get; set; }

        [JsonPropertyName("produto")]
        public virtual PedidoItemProdutoViewModel Produto { get; set; }

        [JsonPropertyName("valorUnitario")]
        public double ValorUnitario { get; set; }

        [JsonPropertyName("quantidade")]
        public double Quantidade { get; set; }

        [JsonPropertyName("valorTotal")]
        public double ValorTotal { get; set; }

        [JsonPropertyName("status")]
        public bool Status { get; set; } = true;
               
        public static implicit operator PedidoItem(PedidoItemViewModel pedidoItemViewModelDTO)
        {
            return new PedidoItem()
            {
                PedidoItemId = pedidoItemViewModelDTO.PedidoItemId,
                PedidoId = pedidoItemViewModelDTO.PedidoId,
                ProdutoId = pedidoItemViewModelDTO.Produto.ProdutoId,
                TabelaPrecoId = pedidoItemViewModelDTO.Produto.TabelaPreco.TabelaPrecoId,
                UnidadeId = pedidoItemViewModelDTO.Produto.Unidade.UnidadeId,
                Nome = pedidoItemViewModelDTO.Produto.Nome,
                ValorUnitario = pedidoItemViewModelDTO.ValorUnitario,
                Quantidade = pedidoItemViewModelDTO.Quantidade,
                ValorTotal = pedidoItemViewModelDTO.ValorTotal
            };
        }
    }
}
