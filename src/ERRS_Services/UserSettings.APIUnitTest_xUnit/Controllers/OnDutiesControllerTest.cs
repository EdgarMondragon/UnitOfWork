using Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserSettings.API.Authorization;
using UserSettings.API.Controllers;
using UserSettings.API.Filters;
using UserSettings.API.Models;
using UserSettings.API.Repositories;
using UserSettings.API.UnitOfWork;
using Xunit;
using FakeItEasy;


namespace UserSettings.APIUnitTest_xUnit.Controllers
{
    [ServiceFilter(typeof(AuthorizationFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class OnDutiesControllerTest : BaseController
    {
        private static IUnitOfWork unitOfWorkContext;
        private static IApplicationContext applicationContext;

        public OnDutiesControllerTest() : base(unitOfWorkContext, applicationContext)
        {            
            _controller = new OnDutiesController(_mockUW.Object, _mockAC.Object);
        }
        AutoMapperTest mapper;
        private readonly Mock<IUnitOfWork> _mockUW = new Mock<IUnitOfWork>();
        private readonly Mock<IApplicationContext> _mockAC = new Mock<IApplicationContext>();
        private OnDutiesController _controller;

        [Fact]
        private void GetAll()
        {
            mapper = new AutoMapperTest();
            mapper.Reset();
            mapper.IsValidConfiguration();
            List<OnDuties> list = new List<OnDuties>();
            MemberPreferences mp = new MemberPreferences();
            var genericIdentity = new API.Authorization.User("4", true, "dummyUser", API.Authorization.User.IarUserTypes.User, "", "", DateTime.Now, 125625, 168);
            _mockUW.Setup(m => m.OnDutiesRepository.GetOnScheduleOnAsync(genericIdentity.SubscriberId)).ReturnsAsync(list);
            _mockUW.Setup(m => m.MemberPreferencesRepository.GetPreferencesByMemberIdAsync(genericIdentity.MemberId, (int)genericIdentity.UserType)).ReturnsAsync(mp);
            _controller = new OnDutiesController(_mockUW.Object, new ApplicationContext(genericIdentity));
            

            var result = _controller.Get();
            Assert.NotNull(result);
        }
    }
}
