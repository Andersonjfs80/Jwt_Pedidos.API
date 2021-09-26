using Domain.Entidades;
using Infrastructure.DBConfiguration.EFCore;
using Infrastructure.Interfaces.Domain;
using Infrastructure.Repositories.Standard.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Domain
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository 
    {
        public CategoriaRepository(ApplicationContext context) : base(context) { }
    }
}
