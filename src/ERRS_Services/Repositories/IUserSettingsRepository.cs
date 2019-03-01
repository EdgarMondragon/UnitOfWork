using DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories
{
    public interface IUserSettingsRepository: IEntityBaseRepository<UserSetting>
    {
        UserSetting GetDefaultSettings();
    }
}
