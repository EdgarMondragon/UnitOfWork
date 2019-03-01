using Dapper;
using DataAccess;
using DataAccess.Infraestructure;
using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Repositories
{
    public class UserSessionInfoRepository : EntityBaseRepository<UserSessionInfo>, IUserSessionInfoRepository
    {
        IConnectionFactory _connectionFactory;
        private readonly ILogger _logger;
        public UserSessionInfoRepository(IConnectionFactory connectionFactory, ILogger<UserSessionInfoRepository> logger) : base(connectionFactory, logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public async Task<UserSessionInfo> GetUserSessionInfoAsync(string token)
        {
            UserSessionInfo entity = new UserSessionInfo();
            using (var connection = _connectionFactory.GetConnection(true))
            {
                connection.Open();
                _logger.LogInformation("Connect to " + connection.Database + " DataBase");
                var entityDTO = await connection.QueryAsync("GetUserSession", new { @Token = token }, commandType: CommandType.StoredProcedure);
                _logger.LogInformation("Entity: " + entityDTO);
                entity = entity.FromDynamic(entityDTO.FirstOrDefault());
                connection.Close();
            }
            return entity;
        }
    }
}
