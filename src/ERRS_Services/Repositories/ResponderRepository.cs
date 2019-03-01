using Dapper;
using DataAccess;
using DataAccess.Infraestructure;
using Entities;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Repositories
{
    public class ResponderRepository : EntityBaseRepository<Responder>, IResponderRepository
    {
        readonly IConnectionFactory _connectionFactory;
        private readonly ILogger _logger;

        public ResponderRepository(IConnectionFactory connectionFactory, ILogger<ResponderRepository> logger) 
            : base(connectionFactory, logger)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<IEnumerable<Responder>> GetRespondersBySubscriberId(long id)
        {
            dynamic respondersE;
            using (var connection = _connectionFactory.GetConnection(true))
            {
                connection.Open();
                respondersE = await connection.QueryAsync("errs.sp_callersinformation_update4",
                    new { @AgencyID = id }, commandType: CommandType.StoredProcedure);
            }
            return Responder.FromDynamic(respondersE);
        }
    }
}
