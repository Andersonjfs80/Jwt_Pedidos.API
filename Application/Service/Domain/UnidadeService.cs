using Application.Interfaces.Domain;
using Application.Service.Standard;
using Domain.Entidades;
using Infrastructure.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Domain
{
    public class UnidadeService : Service<Unidade>, IUnidadeService
    {
        public UnidadeService(IUnidadeRepository repository) : base(repository) {}
    }
}
