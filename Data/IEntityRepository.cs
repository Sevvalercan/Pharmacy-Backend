using Pharmacy_Backend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy_Backend.Core.Data
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        Task<T> GetAsync(Expression<Func<T, bool>> filter);

        Task<List<T>> GetListAsync(Expression<Func<T, bool>> filter = null); 

        Task<T> AddAsync(T entity);  

        Task<T> UpdateAsync(T entity); 

        Task DeleteAsync(T entity); 

        Task SoftDeleteAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;  

        Task<int> CountAsync(Expression<Func<T, bool>> filter = null);
    }
}
