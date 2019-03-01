using DataAccess.Infraestructure;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;
using System.Linq;
using System.Linq.Expressions;
using Dapper;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DataAccess
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T>
       where T : class, IEntityBase, new()
    {
        IConnectionFactory _connectionFactory;
        private readonly ILogger _logger;

        public EntityBaseRepository(IConnectionFactory connectionFactory, ILogger logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public async Task<bool> DeleteAsync(T entity, bool IsIarDB = false)
        {
            bool isSuccess = false;
            using (var connection = _connectionFactory.GetConnection(IsIarDB))
            {
                connection.Open();
                isSuccess =await connection.DeleteAsync(entity);
                connection.Close();
            }

            return isSuccess;
        }

        public async Task<bool> DeleteAllAsync(List<T> entities, bool IsIarDB = false)
        {
            bool isSuccess = false;
            using (var connection = _connectionFactory.GetConnection(IsIarDB))
            {
                connection.Open();
                isSuccess = await connection.DeleteAsync(entities);
                connection.Close();
            }
            return isSuccess;
        }

        public async Task<IEnumerable<T>> ExecuteSPWithOutParametersAsync(string storeProcedure, bool IsIarDB = false)
        {
            List<T> entities = new List<T>();
            using (var connection = _connectionFactory.GetConnection(IsIarDB))
            {
                connection.Open();
                var ntities = await connection.ExecuteAsync(storeProcedure, commandType: CommandType.StoredProcedure);
            }
            return entities;
        }

        public async Task<IEnumerable<T>> ExecuteSPWithParametersAsync(string storeProcedure, object parameters, bool IsIarDB = false)
        {
            List<T> entities = new List<T>();
            using (var connection = _connectionFactory.GetConnection(IsIarDB))
            {
                connection.Open();
                var entityId = await connection.ExecuteAsync(storeProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
            return entities;
        }

        public async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate, bool IsIarDB = false)
        {
            List<T> entities = new List<T>();
            using (var connection = _connectionFactory.GetConnection(IsIarDB))
            {
                connection.Open();
                var allEntities = await connection.GetAllAsync<T>();
                allEntities = allEntities.ToList();
                entities.AddRange(allEntities.AsQueryable().Where(predicate).Select(x => x));
                connection.Close();
            }
            return entities;
        }

        public async Task<T> GetAsync(int id, bool IsIarDB = false)
        {
            T entity = new T();
            using (var connection = _connectionFactory.GetConnection(IsIarDB))
            {
                connection.Open();

                entity = await connection.GetAsync<T>(id);
                connection.Close();

            }
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync(bool IsIarDB = false)
        {
            List<T> entities = new List<T>();
            using (var connection = _connectionFactory.GetConnection(IsIarDB))
            {
                connection.Open();
                var result = await connection.GetAllAsync<T>();
                entities = result.ToList();
                connection.Close();
            }
            return entities;
        }

        public async Task<T> GetByAsync(Expression<Func<T, bool>> predicate, bool IsIarDB = false)
        {
            T entity = new T();
            using (var connection = _connectionFactory.GetConnection(IsIarDB))
            {
                connection.Open();
                var allEntities = await connection.GetAllAsync<T>();
                allEntities = allEntities.ToList();
                entity = allEntities.AsQueryable().Where(predicate).Select(x => x).FirstOrDefault();
                connection.Close();
            }
            return entity;
        }

        public async Task<T> InsertAsync(T entity, bool IsIarDB = false)
        {
            try
            {
                using (var connection = _connectionFactory.GetConnection(IsIarDB))
                {
                    connection.Open();
                    var Id = await connection.InsertAsync(entity);
                    entity.Id = Id;
                    connection.Close();
                }
            }catch(Exception ex)
            {
                _logger.LogError(ex, "error", null);
            }
            return entity;
        }

        public async Task<bool> UpdateAsync(T entity, bool IsIarDB = false)
        {
            bool isSuccess = false;
            using (var connection = _connectionFactory.GetConnection(IsIarDB))
            {
                connection.Open();
                isSuccess = await connection.UpdateAsync(entity);
                connection.Close();
            }
            return isSuccess;
        }

        public async Task<bool> UpdateAllAsync(List<T> entities, bool IsIarDB = false)
        {
            bool isSuccess = false;
            using (var connection = _connectionFactory.GetConnection(IsIarDB))
            {
                connection.Open();
                isSuccess = await connection.UpdateAsync(entities);
                connection.Close();
            }
            return isSuccess;
        }
    }
}
