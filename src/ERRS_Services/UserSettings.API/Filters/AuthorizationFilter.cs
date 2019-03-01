using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserSettings.API.Authorization;
using UserSettings.API.UnitOfWork;

namespace UserSettings.API.Filters
{
    public class AuthorizationFilter : ActionFilterAttribute
    {
        private readonly IUnitOfWork unit;
        private readonly ILogger _logger;

        public AuthorizationFilter(IUnitOfWork unitOfWork, ILogger<AuthorizationFilter> logger)
        {
            unit = unitOfWork;
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            
            var validation = new ValidationTokenMethods(unit, _logger);
            UserSessionInfo result = Task.Run(async () => await  validation.GetUserSessionAsync(context.HttpContext.Request.Cookies["IarSession"])).Result;
            
            User usr = null;
            if (result != null)
            {
                usr = new User ("", true,"", (User.IarUserTypes)result.UserType, "", result.Token, DateTime.Now, result.MemberId, result.SubscriberId);
                var controller = context.Controller as IContextAware;
                controller.ApplicationContext.CurrentUser = usr;
                return;
            }
            else
            {
                _logger.LogError("User don't exist");
                context.Result = new BadRequestResult();
            }
        }

       
    }
}
