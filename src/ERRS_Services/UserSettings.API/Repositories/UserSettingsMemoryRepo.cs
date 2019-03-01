using Entities;
using System.Collections.Generic;
using System.Linq;
using UserSettings.API.Models;

namespace UserSettings.API.Repositories
{
    public class UserSettingsMemoryRepo : IUserSettings
    {
        private readonly List<UserSettingsModel> userSettingsList = new List<UserSettingsModel>();

        public UserSettingsMemoryRepo()
        {
            List<UserDashboardItem> items = new List<UserDashboardItem>
            {
                new UserDashboardItem
                {
                    Item = null,
                    PosX = 0,
                    PosY = 0,
                    Width = 1,
                    Height = 2
                },
                new UserDashboardItem
                {
                    Item = null,
                    PosX = 1,
                    PosY = 0,
                    Width = 1,
                    Height = 2
                }
            };

            userSettingsList.Add(new UserSettingsModel
            {
                Id = 1,
                Items = items
            });
            userSettingsList.Add(new UserSettingsModel
            {
                Id = 2,
                Items = items
            });
            userSettingsList.Add(new UserSettingsModel
            {
                Id = 3,
                Items = items
            });
        }
        public UserSettingsModel Add(UserSettingsModel model)
        {
            model.Id = userSettingsList.Max(p => p.Id) + 1;
            userSettingsList.Add(model);
            return model;
        }

        public IEnumerable<UserSettingsModel> GetAllUsers()
        {
            return userSettingsList;
        }

        public UserSettingsModel GetById(int id)
        {
            return userSettingsList.Single(p => p.Id == id);
        }
    }
}
