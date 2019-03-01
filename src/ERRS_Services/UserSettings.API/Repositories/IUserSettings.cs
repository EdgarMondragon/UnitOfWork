using System.Collections.Generic;
using UserSettings.API.Models;

namespace UserSettings.API.Repositories
{
    public interface IUserSettings
    {
        UserSettingsModel Add(UserSettingsModel model);
        IEnumerable<UserSettingsModel> GetAllUsers();
        UserSettingsModel GetById(int id);
    }
}
