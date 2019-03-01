using DataAccess;
using DataAccess.Infraestructure;
using Entities;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Repositories
{
    public class UserSettingsRepository : EntityBaseRepository<UserSetting>, IUserSettingsRepository
    {
        IConnectionFactory _connectionFactory;
        private readonly ILogger _logger;
        public UserSettingsRepository(IConnectionFactory connectionFactory, ILogger<UserSettingsRepository> logger) : base(connectionFactory, logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public UserSetting GetDefaultSettings()
        {
            UserSetting defaultSettings = new UserSetting
            {
                Id = 0,
                Items = new List<UserDashboardItem>
                {
                    new UserDashboardItem
                    {
                        Item = new DashboardItem() { Name = "NOW RESPONDING" },
                        PosX = 0,
                        PosY = 0,
                        Width = 6,
                        Height = 2
                    },
                    new UserDashboardItem
                    {
                        Item = new DashboardItem() { Name = "ON DUTY" },
                        PosX = 0,
                        PosY = 1,
                        Width = 6,
                        Height = 2
                    }
                }
            };

            return defaultSettings;
        }
    }
}