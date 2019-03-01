using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserSettings.API.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserSettingsRepository UserSettingsRepository { get; }
        IOnDutiesRepository OnDutiesRepository { get; }
        IUserSessionInfoRepository UserSessionInfoRepository { get; }
        IMemberPreferences MemberPreferencesRepository { get; }
        IResponderRepository ResponderRepository { get; }
    }
}
