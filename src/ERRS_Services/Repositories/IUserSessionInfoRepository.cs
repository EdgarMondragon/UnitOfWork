﻿using DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IUserSessionInfoRepository : IEntityBaseRepository<UserSessionInfo>
    {
       Task<UserSessionInfo> GetUserSessionInfoAsync(string token);
    }
}
