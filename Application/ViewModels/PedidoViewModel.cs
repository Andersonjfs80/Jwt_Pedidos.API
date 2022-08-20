using Application.ValidationAttributes;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.ViewModels
{
	public class PedidoViewModel
    {        
        [ValidarCPFCNPJ(ErrorMessage = "teste invalido")]
        [JsonPropertyName("pedidoId")]
        public int PedidoId { get; set; }

        public double ValorTotal { get;  set; }

        [JsonPropertyName("dataCompra")]
        public DateTime DataEmissao { get; set; }

        [JsonPropertyName("dataEntrega")]
        public DateTime? DataSaida { get; set; }
        
        [JsonPropertyName("itens")]
        public virtual List<PedidoItemViewModel> PedidoItens { get; set; }

        public static implicit operator Pedido(PedidoViewModel pedidoViewModelDTO)
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
