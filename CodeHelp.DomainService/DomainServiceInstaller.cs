using CodeHelp.DomainService.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace CodeHelp.DomainService
{
    public class DomainServiceInstaller
    {
        public static void ConfigureContainer(IServiceCollection services)
        {
            services.AddScoped<IDataTablesDomainService, DataTablesDomainService>();
        }
    }
}