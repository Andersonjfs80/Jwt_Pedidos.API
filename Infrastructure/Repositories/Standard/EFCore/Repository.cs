using Infrastructure.Interfaces.Standard;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Standard.EFCore
{  
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext dbContext;

        protected readonly DbSet<TEntity> dbSet;

        protected IDbContextTransaction dbContextTransactionAsync;

		public Repository()
        {
            this.dbContext = Activator.CreateInstance<DbContext>();
            this.dbSet = this.dbContext.Set<TEntity>();
        }

        public Repository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            this.dbSet = this.dbContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            dbSet.Attach(entity).State = EntityState.Added;
            await dbSet.AddAsync(entity);           
        }

        public void Update(TEntity entity)
        {
            dbSet.Attach(entity).State = EntityState.Modified;
            dbSet.Update(entity);
        }

        public void Update(IEnumerable<TEntity> entity)
        {
            dbSet.AttachRange(entity);
            dbSet.UpdateRange(entity);
        }

        public void Delete(TEntity entity)
        {
            dbSet.Attach(entity).State = EntityState.Deleted;
            dbSet.Remove(entity);
        }

        public void Delete(IEnumerable<TEntity> entity)
        {
			dbSet.AttachRange(entity);
            dbSet.RemoveRange(entity);
        }

        public async Task<bool> SaveAsync()
        {
            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await dbSet.Where(filter).AsNoTracking().FirstOrDefaultAsync();
        }

        private IQueryable<TEntity> GenerateIncludeProperties(
            IQueryable<TEntity> query, params string[] includeProperties)
        {
            foreach (string includeProperty in includeProperties)
                query = query.Include(includeProperty);

            return query;
        }

        protected async Task<IEnumerable<TEntity>> GetAllToListAsync(
            IQueryable<TEntity> query,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params string[] includeProperties)
        {
            if (includeProperties != null)
            {
                query = GenerateIncludeProperties(query, includeProperties);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetByIdSingleOrDefaultAsync(
            IQueryable<TEntity> query,
            Expression<Func<TEntity, bool>> filter, 
            params string[] includeProperties)
        {
            if (includeProperties != null)
            {
                query = GenerateIncludeProperties(query, includeProperties);
            }

            return await query.AsNoTracking().SingleOrDefaultAsync(filter);
        }

        public void Dispose()
        {
            dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

		public async Task StartTransactionAsync()
		{
            dbContextTransactionAsync = await dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await dbContextTransactionAsync.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            await dbContextTransactionAsync.RollbackAsync();
        }
	}
}
