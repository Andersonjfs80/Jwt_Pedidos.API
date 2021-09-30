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
    public class CategoriaService : Service<Categoria>, ICategoriaService
    {
        private readonly new ICategoriaRepository _repository;
        public CategoriaService(ICategoriaRepository repository) : base(repository) { }
    }
}
