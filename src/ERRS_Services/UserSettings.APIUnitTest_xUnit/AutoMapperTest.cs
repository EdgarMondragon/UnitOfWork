using Xunit;
using AutoMapper;
using UserSettings.API.Models;

namespace UserSettings.APIUnitTest_xUnit
{
    public class AutoMapperTest
    {
        [Fact]
        public void IsValidConfiguration()
        {
            Mapper.Initialize(config => config.AddProfiles(typeof(Source<>)));
            Mapper.AssertConfigurationIsValid();
        }

        public void Reset()
        {
            Mapper.Reset();
        }
    }
    public class Source<T>
    {
        public T Value { get; set; }
    }

}