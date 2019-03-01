using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using UserSettings.API.Models;
using UserSettings.API.Repositories;
using UserSettings.API.UnitOfWork;
using Entities;
using UserSettings.API.Filters;
using UserSettings.API.Authorization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UserSettings.API.Controllers
{
    [ServiceFilter(typeof(AuthorizationFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class UserSettingsController : BaseController
    {

        public UserSettingsController(IUserSettings repo, IUnitOfWork unitOfWorkContext, IApplicationContext applicationContext) : base(unitOfWorkContext, applicationContext)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var userSettings = await UnitOfWork.UserSettingsRepository.GetAllAsync();
            if (!userSettings.Any())
                return new NoContentResult();
            
            return new ObjectResult(userSettings);
        }

        [HttpGet("{id}")]
        public async Task<UserSettingsModel> GetAsync(int id)
        {
            UserSettingsModel defaultUserSettings= new UserSettingsModel();
            var userSettings = await UnitOfWork.UserSettingsRepository.GetByAsync(x => x.memberid == ApplicationContext.CurrentUser.MemberId);
            if (userSettings != null)
            {
                defaultUserSettings = JsonConvert.DeserializeObject<UserSettingsModel>(userSettings.ModelJson);
                defaultUserSettings.Id = userSettings.Id;
            }
            else
            {
                defaultUserSettings = Mapper.Map<UserSettingsModel>(UnitOfWork.UserSettingsRepository.GetDefaultSettings());
            }
            return defaultUserSettings;
        }

        [HttpPost]
        public async Task<UserSettingsModel> AddAsync([FromBody]UserSettingsModel model)
        {
            Entities.UserSetting objToSave = new Entities.UserSetting();
            objToSave.agencyid = ApplicationContext.CurrentUser.SubscriberId;
            objToSave.ColumnSize = 0;
            objToSave.Controls = true;
            objToSave.memberid = ApplicationContext.CurrentUser.MemberId;
            objToSave.Theme = 0;
            var json = JsonConvert.SerializeObject(model);
            objToSave.ModelJson = json;
            UserSettingsModel userSettingToRetrieve;
            var getUserSettings = await UnitOfWork.UserSettingsRepository.GetByAsync(x => x.memberid == ApplicationContext.CurrentUser.MemberId);
            if (getUserSettings != null)
            {
                if (getUserSettings.ModelJson.Equals(string.Empty))
                {
                    var userSetting = await UnitOfWork.UserSettingsRepository.InsertAsync(objToSave);
                    userSettingToRetrieve = JsonConvert.DeserializeObject<UserSettingsModel>(userSetting.ModelJson);
                }
                else 
                {
                    objToSave.Id = getUserSettings.Id;
                    bool success = await UnitOfWork.UserSettingsRepository.UpdateAsync(objToSave);
                    userSettingToRetrieve = JsonConvert.DeserializeObject<UserSettingsModel>(objToSave.ModelJson);
                }
            }
            else
            {
                var userSetting = await UnitOfWork.UserSettingsRepository.InsertAsync(objToSave);
                if (userSetting != null)
                    userSettingToRetrieve = JsonConvert.DeserializeObject<UserSettingsModel>(userSetting.ModelJson);
                else
                    userSettingToRetrieve = Mapper.Map<UserSettingsModel>(UnitOfWork.UserSettingsRepository.GetDefaultSettings());
            }
            return userSettingToRetrieve;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody]UserSettingsModel model)
        {
            var setting = Mapper.Map<UserSettingsModel, UserSetting>(model);
            Entities.UserSetting objToSave = new Entities.UserSetting();
            objToSave.agencyid = ApplicationContext.CurrentUser.SubscriberId;
            objToSave.ColumnSize = 0;
            objToSave.Controls = true;
            objToSave.memberid = ApplicationContext.CurrentUser.MemberId;
            objToSave.Theme = 0;
            var json = JsonConvert.SerializeObject(setting);
            objToSave.ModelJson = json;
            //var userSetting = await UnitOfWork.UserSettingsRepository.InsertAsync(objToSave);
            bool success = await UnitOfWork.UserSettingsRepository.UpdateAsync(objToSave);
            return new ObjectResult(success);
        }

        [HttpGet("default")]
        public async Task<UserSettingsModel> GetDefaultSettingsAsync(bool isChecked)
        {            
            UserSettingsModel defaultUserSettings;
            if (isChecked)
            {
                defaultUserSettings = Mapper.Map<UserSettingsModel>(UnitOfWork.UserSettingsRepository.GetDefaultSettings());
            }
            else
            {
                var getUserSettings = await UnitOfWork.UserSettingsRepository.GetByAsync(x => x.memberid == ApplicationContext.CurrentUser.MemberId);
                if (getUserSettings != null)
                {
                    if (getUserSettings.ModelJson.Equals(string.Empty))
                    {
                        defaultUserSettings = Mapper.Map<UserSettingsModel>(UnitOfWork.UserSettingsRepository.GetDefaultSettings());
                    }
                    else
                    {
                        defaultUserSettings = JsonConvert.DeserializeObject<UserSettingsModel>(getUserSettings.ModelJson);
                        defaultUserSettings.Id = getUserSettings.Id;
                    }
                }
                else
                {
                    defaultUserSettings = Mapper.Map<UserSettingsModel>(UnitOfWork.UserSettingsRepository.GetDefaultSettings());
                }
            }
            return defaultUserSettings;
        }
    }
}