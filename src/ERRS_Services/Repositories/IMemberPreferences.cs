using DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IMemberPreferences : IEntityBaseRepository<MemberPreferences>
    {
        Task<MemberPreferences> GetPreferencesByMemberIdAsync(long id, int UserType);
    }
}
