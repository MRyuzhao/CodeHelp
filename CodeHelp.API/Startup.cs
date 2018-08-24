using CodeHelp.Common.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace CodeHelp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //webapi配置identity server就需要对token进行验证
            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();

            //Bearer作为默认模式.
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    //Authority指定Authorization Server的地址.
                    options.Authority = "http://localhost:5000";
                    //本地运行, 所以就不使用https,如果是生产环境, 一定要使用https.
                    options.RequireHttpsMetadata = false;
                    //ApiName要和Authorization Server里面配置ApiResource的name一样.
                    options.ApiName = "codeHelpApis";
                });

            services.AddCors(options =>
            {
                options.AddPolicy("CodeHelpCorePolicy",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader()
                                      .AllowCredentials()
                    );
            });

            services.AddMvc();

            #region 添加Swagger
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Shows UseCors with named policy.
            app.UseCors("CodeHelpCorePolicy");

            //start logging to the console
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseErrorHandling();

            app.UseAuthentication();

            app.UseMvc();

            #region 使用Swagger
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CodeHelp API V1");
                //c.RoutePrefix = string.Empty;
            });
            #endregion
        }
    }
}
