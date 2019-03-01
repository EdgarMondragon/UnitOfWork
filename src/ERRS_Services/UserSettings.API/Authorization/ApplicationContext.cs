using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserSettings.API.Authorization
{
    public class ApplicationContext : IApplicationContext
    {
        public User CurrentUser { get; set; }
        public bool Unrestricted { get; set; }

        public ApplicationContext(User currentUser)
        {
            CurrentUser = currentUser;
        }

        public ApplicationContext()
        {
            CurrentUser = new User();
        }
    }

   
}
