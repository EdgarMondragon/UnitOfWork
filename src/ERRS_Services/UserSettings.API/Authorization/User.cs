using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace UserSettings.API.Authorization
{
    public class User : IIdentity
    {
        public string AuthenticationType { get; private set; }
        public bool IsAuthenticated { get; private set; }
        public string Name { get; set; }
        public IarUserTypes UserType { get; private set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public DateTime CreatedOn { get; private set; }
        public int MemberId { get; set; }
        public int SubscriberId { get; set; }

        public enum IarUserTypes
        {
            Administrator = 0,
            MasterAdmin,
            Dispatcher,
            User,
            DispatcherUser,
            Apparatus
        }

        public User()
        {
        }

        public User(string authenticationType, bool isAuthenticated, string name, IarUserTypes userType, string password, string apiKey, DateTime createdOn, int memberId, int subscriberId)
        {
            AuthenticationType = authenticationType;
            IsAuthenticated = isAuthenticated;
            Name = name;
            UserType = userType;
            Password = password;
            Token = apiKey;
            CreatedOn = createdOn;
            MemberId = memberId;
            SubscriberId = subscriberId;
        }
    }
}
