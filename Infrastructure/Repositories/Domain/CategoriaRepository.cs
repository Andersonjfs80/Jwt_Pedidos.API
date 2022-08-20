using Domain.Entidades;
using Infrastructure.DBConfiguration.EFCore;
using Infrastructure.Interfaces.Domain;
using Infrastructure.Repositories.Standard.EFCore;

namespace Infrastructure.Repositories.Domain
{
	public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository 
    {
        public CategoriaRepository(ApplicationContext context) : base(context) { }
    }
}
