using System;
using System.Collections.Generic;
using System.Linq;
using CodeHelp.Common.Mapper;
using CodeHelp.QueryService.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace CodeHelp.API
{
    public class MapperInstaller
    {
        public static void ConfigureContainer(IServiceCollection services)
        {
            services.AddSingleton<IMap, Mapper>();
            //Register all the AutoMapper profiles
            var typeAutoMapperProfile = typeof(AutoMapper.Profile);

            var allQueryServiceProfileTypes = new List<Type>();
            allQueryServiceProfileTypes.AddRange(typeof(DataTablesViewModelAutoMapper).Assembly.GetTypes().Where(
                x => x != typeAutoMapperProfile
                     && typeAutoMapperProfile.IsAssignableFrom(x)
            ));

            AutoMapper.Mapper.Initialize(cfg =>
            {
                allQueryServiceProfileTypes.ForEach(x =>
                {
                    var profile = Activator.CreateInstance(x) as AutoMapper.Profile;
                    cfg.AddProfile(profile);
                });
                //cfg.AddProfile<DataTablesViewModelAutoMapper>();
                //cfg.AddProfile<TableColumnsViewModelAutoMapper>();
            });
        }
    }
}
