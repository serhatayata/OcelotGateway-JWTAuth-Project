using Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T:class, IEntity, new()
    {
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null);
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> filter = null);
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter = null);
        IQueryable<T> GetAsQueryable(Expression<Func<T, bool>> filter = null);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
