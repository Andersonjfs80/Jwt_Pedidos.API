using Application.Interfaces.Standard;
using Domain.ViewModels;
using Domain.Entidades;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Interfaces.Domain
{
    public interface IPedidoService : IService<Pedido> 
    {
        Task<PedidoViewModel> ProcessarPedido(PedidoViewModel pedido);
    }
}
