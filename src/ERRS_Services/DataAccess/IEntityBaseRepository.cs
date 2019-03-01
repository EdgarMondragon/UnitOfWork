using Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        Task<IEnumerable<T>> GetAllAsync(bool IsIarDB = false);
        Task<T> GetAsync(int id, bool IsIarDB = false);
        Task<T> GetByAsync(Expression<Func<T, bool>> predicate, bool IsIarDB = false);
        Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate, bool IsIarDB = false);
        Task<T> InsertAsync(T entity, bool IsIarDB = false);
        Task<bool> UpdateAsync(T entity, bool IsIarDB = false);
        Task<bool> UpdateAllAsync(List<T> entities, bool IsIarDB = false);
        Task<bool> DeleteAsync(T entity, bool IsIarDB = false);
        Task<bool> DeleteAllAsync(List<T> entities, bool IsIarDB = false);
        Task<IEnumerable<T>> ExecuteSPWithOutParametersAsync(string storeProcedure, bool IsIarDB = false);
        Task<IEnumerable<T>> ExecuteSPWithParametersAsync(string storeProcedure, object parameters, bool IsIarDB = false);
    }
}
