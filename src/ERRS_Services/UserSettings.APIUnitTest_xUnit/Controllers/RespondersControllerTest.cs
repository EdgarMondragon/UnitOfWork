using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using UserSettings.API.Authorization;
using UserSettings.API.Controllers;
using UserSettings.API.Filters;
using UserSettings.API.UnitOfWork;
using Xunit;

namespace UserSettings.APIUnitTest_xUnit.Controllers
{
    [ServiceFilter(typeof(AuthorizationFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class RespondersControllerTest : BaseController
    {
        private static IUnitOfWork unitOfWorkContext;
        private static IApplicationContext applicationContext;
        private readonly Mock<IUnitOfWork> _mockUW = new Mock<IUnitOfWork>();
        private readonly Mock<IApplicationContext> _mockAC = new Mock<IApplicationContext>();
        private RespondersController _controller;

        public RespondersControllerTest() : base(unitOfWorkContext, applicationContext)
        {
            _controller = new RespondersController(_mockUW.Object, _mockAC.Object);
        }

        [Fact]
        public async Task Get()
        {
            var genericIdentity = new User("4", true, "dummyUser", API.Authorization.User.IarUserTypes.User, "", "", DateTime.Now, 125625, 168);

            _mockUW.Setup(m => m.ResponderRepository.GetRespondersBySubscriberId(_controller.ApplicationContext.CurrentUser.SubscriberId));
            _mockUW.Setup(m => m.MemberPreferencesRepository.GetPreferencesByMemberIdAsync(_controller.ApplicationContext.CurrentUser.MemberId,
                (int)_controller.ApplicationContext.CurrentUser.UserType));

            _controller = new RespondersController(_mockUW.Object, new ApplicationContext(genericIdentity));

            var actionResult = await _controller.Get();
            Assert.NotNull(actionResult);
        }
    }
}
