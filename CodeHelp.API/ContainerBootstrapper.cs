using System;
using CodeHelp.Common;
using CodeHelp.Data.Dapper;
using CodeHelp.DomainService;
using CodeHelp.QueryService;
using CodeHelp.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace CodeHelp.API
{
    public class ContainerBootstrapper
    {
        internal static Action<IServiceCollection> ConfigureServices()
        {
            return services =>
            {
                CommonInstaller.ConfigureContainer(services);
                DataInstaller.ConfigureContainer(services);
                RepositoryInstaller.ConfigureContainer(services);
                MapperInstaller.ConfigureContainer(services);
                QueryServiceInstaller.ConfigureContainer(services);
                DomainServiceInstaller.ConfigureContainer(services);
            };
        }
    }
}
