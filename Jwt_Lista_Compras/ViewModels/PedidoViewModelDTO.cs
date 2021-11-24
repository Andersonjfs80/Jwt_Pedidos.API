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
    public class PedidoViewModelDTO
    {
        [JsonPropertyName("ItemId")]
        public int PedidoId { get; set; }

        public double ValorTotal { get;  set; }

        [JsonPropertyName("DataCompra")]
        public DateTime DataEmissao { get; set; }

        [JsonPropertyName("DataEntrega")]
        public DateTime? DataSaida { get; set; }
        
        [JsonPropertyName("Itens")]
        public virtual List<PedidoItemViewModelDTO> PedidoItens { get; set; }

        public static implicit operator Pedido(PedidoViewModelDTO pedidoViewModelDTO)
        {
            return new Pedido()
            {
                PedidoId = pedidoViewModelDTO.PedidoId,
                DataEmissao = pedidoViewModelDTO.DataEmissao,
                DataSaida = pedidoViewModelDTO.DataSaida
            };
        }
    }
}
