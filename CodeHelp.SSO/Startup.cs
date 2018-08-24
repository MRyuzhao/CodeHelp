using System;
using CodeHelp.Common.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CodeHelp.SSO
{
    public class Startup
    {
        private readonly Config _config;

        public Startup(Config config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // configure identity server with in-memory stores, keys, clients and scopes
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                //.AddSigningCredential(new X509Certificate2(@"D:\Projects\test\socialnetwork.pfx", "password"))
                .AddInMemoryIdentityResources(_config.GetIdentityResources())
                .AddInMemoryApiResources(_config.GetApiResources())
                .AddInMemoryClients(_config.GetClients())
                .AddTestUsers(_config.GetUsers());

            services.AddAuthentication("CodeHelpSSO")
                .AddCookie("CodeHelpSSO", options =>
                {
                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = TimeSpan.FromHours(5);
                    options.Cookie.Expiration = TimeSpan.FromHours(5);
                });

            services.AddMvc();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Debug);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseIdentityServer();
            //app.UseAuthentication();

            app.UseErrorHandling();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
