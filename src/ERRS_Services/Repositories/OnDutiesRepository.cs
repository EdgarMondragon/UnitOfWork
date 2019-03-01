using Dapper;
using DataAccess;
using DataAccess.Infraestructure;
using Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OnDutiesRepository : EntityBaseRepository<OnDuties>, IOnDutiesRepository
    {
        IConnectionFactory _connectionFactory;
        public readonly IMemberPreferences _memberPreferencesRepository;
        private readonly ILogger _logger;
        public OnDutiesRepository(IConnectionFactory connectionFactory, ILogger<UserSettingsRepository> logger) : base(connectionFactory, logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public async Task<List<OnDuties>> GetOnScheduleOnAsync(long agencyid)
        {
            IEnumerable<dynamic> entities;
            try
            {
                using (var connection = _connectionFactory.GetConnection(true))
                {
                    connection.Open();
                    entities = await connection.QueryAsync("sp_getonschedule_new", new { @agencyid = agencyid }, commandType: CommandType.StoredProcedure);
                }
                return Entities.OnDuties.FromDynamic(entities);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);
                return null;
            }
        }
    }
}
