using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Jwt_Pedidos_v1.API.ViewModels
{
    public class PedidoItemViewModelDTO
    {
        public PedidoItemViewModelDTO()
        {
            Produto = new PedidoItemProdutoViewModelDTO();
        }

        [JsonPropertyName("ItemId")]
        public int PedidoItemId { get; set; }

        public int PedidoId { get; set; }

        [JsonIgnore]
        public virtual Pedido Pedido { get; set; }

        [JsonPropertyName("Produto")]
        public virtual PedidoItemProdutoViewModelDTO Produto { get; set; }

        public double ValorUnitario { get; set; }

        public double Quantidade { get; set; }

        public double ValorTotal { get; set; }

        public bool Status { get; set; } = true;
               
        public static implicit operator PedidoItem(PedidoItemViewModelDTO pedidoItemViewModelDTO)
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
