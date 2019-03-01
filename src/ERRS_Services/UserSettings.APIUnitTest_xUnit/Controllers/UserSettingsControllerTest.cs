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

namespace UserSettings.APIUnitTest_xUnit
{
    [ServiceFilter(typeof(AuthorizationFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class UserSettingsControllerTest: BaseController
    {
        private static IUnitOfWork unitOfWorkContext;
        private static IApplicationContext applicationContext;
        public UserSettingsControllerTest(): base(unitOfWorkContext, applicationContext)
        {
            _controller = new UserSettingsController(_mock.Object,_mockUW.Object,_mockAC.Object);           
        }
        AutoMapperTest mapper; 
        private readonly Mock<IUserSettings> _mock = new Mock<IUserSettings>();
        private readonly Mock<IUnitOfWork> _mockUW = new Mock<IUnitOfWork>();
        private readonly Mock<IApplicationContext> _mockAC = new Mock<IApplicationContext>();
        private  UserSettingsController _controller;
        private readonly BaseController _baseController;
        private readonly Mock<BaseController> _mockBC = new Mock<BaseController>();

        private static List<UserDashboardItem> items = new List<UserDashboardItem>
        {
            new UserDashboardItem
            {
                Item = null,
                PosX = 0,
                PosY = 0,
                Width = 1,
                Height = 2
            },
            new UserDashboardItem
            {
                Item = null,
                PosX = 1,
                PosY = 0,
                Width = 1,
                Height = 2
            }
        };
        
        readonly IEnumerable<UserSettingsModel> _listUserSettings = new List<UserSettingsModel> {
            new UserSettingsModel {
                Id = 1,
                Items = items
            }
        };

        readonly Task<IEnumerable<UserSetting>> _listEntitiesUserSettings;
        //= new List<UserSetting> {
        //    new UserSetting {
        //        Id = 1,
        //        Items = items
        //    }
        // };

        readonly UserSettingsModel userSetting = new UserSettingsModel()
        {
            Id = 1,
            Items = items
        };

        readonly UserSetting entityUserSetting = new UserSetting()
        {
            Id = 1,
            Items = items
        };

        [Fact]
        private void GetAll()
        {
            mapper = new AutoMapperTest();
            mapper.Reset();
            mapper.IsValidConfiguration();
            _mock.Setup(m => m.GetAllUsers())
                 .Returns(_listUserSettings);
            _mockUW.Setup(m => m.UserSettingsRepository.GetAllAsync(false)).Returns( _listEntitiesUserSettings);
            var actionResult =  _controller.GetAllAsync();
            Assert.NotNull(actionResult);
        }

        [Fact]
        private async Task GetAsync()
        {
            mapper = new AutoMapperTest();
            mapper.Reset();
            mapper.IsValidConfiguration();
            UserSettingsModel dummyUSM = new UserSettingsModel();            
            Entities.UserSetting userSettingsReturn = new UserSetting();
            var genericIdentity = new API.Authorization.User("4", true, "dummyUser", API.Authorization.User.IarUserTypes.User, "", "", DateTime.Now, 125625, 168);
            _controller = new UserSettingsController(_mock.Object, _mockUW.Object, new ApplicationContext(genericIdentity));
            _mockBC.Setup(m => m.Accepted());
            User user = new User();
            _mock.Setup(m => m.GetById(It.IsAny<int>()))
                 .Returns(dummyUSM);
            Assert.NotNull(dummyUSM);
            _mockUW.Setup(m => m.UserSettingsRepository.GetDefaultSettings()).Returns(userSettingsReturn);
            _mockUW.Setup(m => m.UserSettingsRepository.GetByAsync(x => x.Id == It.IsAny<int>(), false));
            var actionResult =await _controller.GetAsync(1);
            Assert.NotNull(actionResult);
        }

        [Fact]
        private async Task AddAsync()
        {
            mapper = new AutoMapperTest();
            mapper.Reset();
            mapper.IsValidConfiguration();
            var genericIdentity = new API.Authorization.User("4",true,"dummyUser",API.Authorization.User.IarUserTypes.User,"","",DateTime.Now,125625,168);
            
            _controller = new UserSettingsController(_mock.Object, _mockUW.Object, new ApplicationContext(genericIdentity));
            UserSettingsModel dummyUSM = new UserSettingsModel();
            UserSetting dummyUS = new UserSetting();
            _mock.Setup(m => m.GetById(It.IsAny<Int32>()));
            _mock.Setup(m => m.Add(userSetting));
            _mockUW.Setup(m => m.UserSettingsRepository.InsertAsync(entityUserSetting, false));
            userSetting.Id = 2;
            userSetting.Items = items;
            var actionResult = await _controller.AddAsync(A.Fake<UserSettingsModel>());
            Assert.Null(actionResult);
        }
    }
}
