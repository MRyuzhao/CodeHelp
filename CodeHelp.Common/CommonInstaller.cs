using Microsoft.Extensions.DependencyInjection;

namespace CodeHelp.Common
{
    public class CommonInstaller
    {
        public static void ConfigureContainer(IServiceCollection services)
        {
            services.AddSingleton<ITimeSource, TimeSource>();
        }
    }
}