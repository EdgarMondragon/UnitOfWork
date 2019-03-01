using Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserSettings.API.UnitOfWork;

namespace UserSettings.API.Authorization
{
    public class ValidationTokenMethods
    {
        private readonly IUnitOfWork unit;
        private readonly ILogger _logger;
        public ValidationTokenMethods(IUnitOfWork unitOfWork, ILogger logger)
        {
            unit = unitOfWork;
            _logger = logger;
        }

        public async Task<UserSessionInfo> GetUserSessionAsync(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                _logger.LogError("Token is empty");
                return null;
            }
            var userInfo = await unit.UserSessionInfoRepository.GetUserSessionInfoAsync(token.Replace("Token=", ""));

            return userInfo;

        }
    }
}
