using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserSettings.API.Authorization
{
    public interface IApplicationContext
    {
        User CurrentUser { get; set; }
        bool Unrestricted { get; set; }
    }

    public interface IContextAware
    {
        IApplicationContext ApplicationContext { get; }
    }
}
