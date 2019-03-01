using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserSettings.API.Filters
{
    public class GlobalFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
