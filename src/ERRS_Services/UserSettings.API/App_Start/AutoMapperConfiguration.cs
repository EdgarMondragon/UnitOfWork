using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserSettings.API.App_Start
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x => x.CreateMap(typeof(Source<>), typeof(Destination<>)));
        }
    }

    public class Source<T>
    {
        public T Value { get; set; }
    }

    public class Destination<T>
    {
        public T Value { get; set; }
    }
}
