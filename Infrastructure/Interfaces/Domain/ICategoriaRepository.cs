using Domain.Entidades;
using Infrastructure.Interfaces.Standard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Domain
{
    public interface ICategoriaRepository : IRepository<Categoria> {}
}
