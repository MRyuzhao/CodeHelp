using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace CodeHelp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Run();
        }

        public static IWebHost CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(ContainerBootstrapper.ConfigureServices())
                .UseStartup<Startup>()
                .Build();
    }
}
