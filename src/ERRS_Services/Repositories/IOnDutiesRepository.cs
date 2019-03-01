using DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IOnDutiesRepository : IEntityBaseRepository<OnDuties>
    {
        Task<List<OnDuties>> GetOnScheduleOnAsync(long agencyid);
    }
}
