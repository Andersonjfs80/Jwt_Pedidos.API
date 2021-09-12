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
    public interface ICategoriaItemRepository : IRepository<CategoriaItem>
    {
        Task<IEnumerable<CategoriaItem>> GetAllIncludingAsync(    
            Expression<Func<CategoriaItem, bool>> filter = null,   
            Func<IQueryable<CategoriaItem>, IOrderedQueryable<CategoriaItem>> orderBy = null,    
            params string[] includeProperties);

        Task<CategoriaItem> GetByIdIncludingAsync(
            Expression<Func<CategoriaItem, bool>> filter, params string[] includeProperties);
    }
}
