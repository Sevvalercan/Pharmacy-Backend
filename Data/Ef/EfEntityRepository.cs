using Pharmacy_Backend.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Pharmacy_Backend.Core.Data.Ef
{
    public class EfEntityRepository<TEntity, TContext> : IEntityRepository<TEntity>
       where TEntity : class, IEntity, new()
      where TContext : DbContext, new()
    {
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await using var context = new TContext();
            var addedEntity = context.Entry(entity);
            addedEntity.State = EntityState.Added;
            await context.SaveChangesAsync();
            return addedEntity.Entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await using var context = new TContext();
            var deletedEntity = context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            await using var context = new TContext();
            var entry = context.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                context.Attach(entity);
            }

            entity.Status = false; // Status özelliği false olarak ayarlandı
            entity.DeletedDate = DateTime.UtcNow;

            // EntityState.Modified kullanılıyor, çünkü varlığın güncellendiğini belirtmek istiyoruz
            entry.State = EntityState.Modified;

            await context.SaveChangesAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            await using var context = new TContext();
            return await context.Set<TEntity>().SingleOrDefaultAsync(filter);
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            await using var context = new TContext();
            return filter == null
                ? await context.Set<TEntity>().ToListAsync()
                : await context.Set<TEntity>().Where(filter).ToListAsync();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            await using var context = new TContext();
            return filter == null
                ? await context.Set<TEntity>().CountAsync()
                : await context.Set<TEntity>().CountAsync(filter);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await using var context = new TContext();
            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return updatedEntity.Entity;
        }

    }
}