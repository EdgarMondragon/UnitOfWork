using Microsoft.AspNetCore.Mvc;
using UserSettings.API.UnitOfWork;
using Entities;
using System.Collections.Generic;
using UserSettings.API.Filters;
using UserSettings.API.Authorization;
using System.Threading.Tasks;

namespace UserSettings.API.Controllers
{
    [ServiceFilter(typeof(AuthorizationFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class OnDutiesController : BaseController
    {

        public OnDutiesController(IUnitOfWork unitOfWorkContext, IApplicationContext applicationContext) : base(unitOfWorkContext, applicationContext)
        {
        }
        [HttpGet]
        public async Task<List<OnDuties>> Get()
        {
            List<OnDuties> onDutiesList = new List<OnDuties>();
            int memberid =ApplicationContext.CurrentUser.MemberId;
            int userType =  (int)ApplicationContext.CurrentUser.UserType;
            long agencyid =  (long)ApplicationContext.CurrentUser.SubscriberId;
            onDutiesList = await UnitOfWork.OnDutiesRepository.GetOnScheduleOnAsync(agencyid);
            MemberPreferences memberPreferences = await UnitOfWork.MemberPreferencesRepository.GetPreferencesByMemberIdAsync(memberid, userType);
            string onDutySortExpression = memberPreferences == null ? string.Empty : (memberPreferences.OnScheduleSort ?? string.Empty);
            onDutiesList= OnDuties.ForResponse(onDutiesList, onDutySortExpression);            
            return onDutiesList;
        }
    }
}
