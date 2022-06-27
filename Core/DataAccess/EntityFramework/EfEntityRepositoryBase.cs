using Microsoft.EntityFrameworkCore;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            await using var context = new TContext();
            return await context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(filter);
        }

        //public async Task<IReadOnlyList<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null)
        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            await using var context = new TContext();
            return filter == null
                ? await context.Set<TEntity>().ToListAsync()
                : await context.Set<TEntity>().Where(filter).ToListAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            await using var context = new TContext();
            return filter != null && await context.Set<TEntity>().AnyAsync(filter);
        }

        public async Task AddAsync(TEntity entity)
        {
            await using var context = new TContext();
            var addedEntity = context.Entry(entity);
            addedEntity.State = EntityState.Added;
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await using var context = new TContext();
            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(TEntity entity)
        {
            await using var context = new TContext();
            var deletedEntity = context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetAsQueryable(Expression<Func<TEntity, bool>> filter = null)
        {
            using var context = new TContext();
            return filter == null
                ? context.Set<TEntity>().AsQueryable()
                : context.Set<TEntity>().Where(filter).AsQueryable();
        }
    }
}
