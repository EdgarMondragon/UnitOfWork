using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class UserSessionInfo: IEntityBase
    {
        public int MemberId { get; set; }
        public int SubscriberId { get; set; }
        public int UserType { get; set; }
        public string Token { get; set; }
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }

        public UserSessionInfo FromDynamic(dynamic dynamic)
        {
            return dynamic == null ? null :
                new UserSessionInfo
                {
                    MemberId = dynamic.MemberID,
                    SubscriberId = dynamic.SubscriberID,
                    UserType =dynamic.UserType,
                    CreatedOn = dynamic.CreatedOn,
                    Token = dynamic.Token.ToString()
                };
        }
    }
}
