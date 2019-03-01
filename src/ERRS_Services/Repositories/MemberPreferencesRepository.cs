using Dapper;
using DataAccess;
using DataAccess.Infraestructure;
using Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class MemberPreferencesRepository : EntityBaseRepository<MemberPreferences>, IMemberPreferences
    {
        IConnectionFactory _connectionFactory;
        private readonly ILogger _logger;
        public MemberPreferencesRepository(IConnectionFactory connectionFactory, ILogger<UserSettingsRepository> logger) : base(connectionFactory, logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }
        public async Task<MemberPreferences> GetPreferencesByMemberIdAsync(long id, int UserType)
        {            
            int intentos = 1;
            string sp = UserType == 3 || UserType == 0 ? "GetMemberPreferences" : "GetApparatusPreferences";            
            while (intentos <= 5)
            {
                try
                {
                    using (var connection = _connectionFactory.GetConnection(true))
                    {
                        connection.Open();
                        var entities =await connection.QueryAsync(sp, new { @MemberID=id }, commandType: CommandType.StoredProcedure);
                        return MemberPreferences.FromDynamic(entities.FirstOrDefault());
                    }
                }
                catch (Exception ex)
                {
                    System.Threading.Thread.Sleep(5000);
                    _logger.LogError(ex, ex.Message, null);
                    intentos++;
                }
            }
            return MemberPreferences.FromDynamic(null);
        }
    }
}
