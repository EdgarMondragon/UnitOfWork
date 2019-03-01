using Entities;
using System.Collections.Generic;

namespace UserSettings.API.Models
{
    public class UserSettingsModel
    {
        public int Id { get; set; }
        public List<UserDashboardItem> Items { get; set; }
    }
}
