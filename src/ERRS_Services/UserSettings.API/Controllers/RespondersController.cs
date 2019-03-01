using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserSettings.API.Authorization;
using UserSettings.API.Filters;
using UserSettings.API.UnitOfWork;

namespace UserSettings.API.Controllers
{
    [ServiceFilter(typeof(AuthorizationFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class RespondersController : BaseController
    {
        public RespondersController(IUnitOfWork unitOfWorkContext, IApplicationContext applicationContext) 
            : base(unitOfWorkContext, applicationContext)
        {
        }

        [HttpGet]
        public async Task<IEnumerable<Responder>> Get()
        {
            string nowRespondingSortExpression = string.Empty;
            // TODO get subscriberId from context
            IEnumerable<Responder> responders = 
                await UnitOfWork.ResponderRepository.GetRespondersBySubscriberId(ApplicationContext.CurrentUser.SubscriberId);
            // TODO get user type from context (int)Context.CurrentUser.User.UserType
            MemberPreferences preference = 
                await UnitOfWork.MemberPreferencesRepository.GetPreferencesByMemberIdAsync(ApplicationContext.CurrentUser.MemberId, (int)ApplicationContext.CurrentUser.UserType);
            if (preference != null)
            {
                nowRespondingSortExpression = (preference.NowRespondingSort ?? string.Empty);
                // this is a temporary for a confusion in the ios client, 
                // we simply reverse the sorts for date/time fields
                nowRespondingSortExpression = nowRespondingSortExpression.Contains("callingtime asc")
                                                  ? nowRespondingSortExpression.Replace("callingtime asc",
                                                                                        "callingtime desc")
                                                  : nowRespondingSortExpression.Replace("callingtime desc",
                                                                                        "callingtime asc");
                nowRespondingSortExpression = nowRespondingSortExpression.Contains("eta asc")
                                                 ? nowRespondingSortExpression.Replace("eta asc", "eta desc")
                                                 : nowRespondingSortExpression.Replace("eta desc", "eta asc");
            }
            return Responder.ForResponse(responders, nowRespondingSortExpression);
        }
    }
}
