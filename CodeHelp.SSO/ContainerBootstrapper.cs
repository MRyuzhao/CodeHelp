using System;
using CodeHelp.Common;
using Microsoft.Extensions.DependencyInjection;

namespace CodeHelp.SSO
{
    public class ContainerBootstrapper
    {
        internal static Action<IServiceCollection> ConfigureServices()
        {
            return services =>
            {
                CommonInstaller.ConfigureContainer(services);
                services.AddSingleton<Config, Config>();
            };
        }
    }
}
