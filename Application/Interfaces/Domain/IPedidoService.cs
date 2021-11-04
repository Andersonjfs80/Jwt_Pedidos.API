using Application.Interfaces.Standard;
using Domain.Entidades;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Interfaces.Domain
{
    public interface IPedidoService : IService<Pedido> { }
}
