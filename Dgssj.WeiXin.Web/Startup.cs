using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Dgssj.WeiXin.Model;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using log4net.Repository;
using log4net;
using log4net.Config;
using System.IO;

namespace Dgssj.WeiXin.Web
{
    public class Startup
    {
        public static ILoggerRepository repository { get; set; }
        public Startup(IHostingEnvironment env)
        {
            Common.LoggerHelper.Info("InfoLog Test");
            Common.LoggerHelper.Error("ErrorLog Test");
            Common.LoggerHelper.Debug("DebugLog Test");
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton<IWeChatConfig>(Configuration.GetSection("Wechat").Get<WeChatConfig>());
            services.AddOptions();
            services.Configure<SysConfig>(Configuration.GetSection("SystemConfig"));
            //Session服务
            services.AddSession();
            // 添加认证Cookie信息
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
             {
                 options.LoginPath = new PathString("/Account/Login");
                 //options.AccessDeniedPath = new PathString("/denied");
             });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
          
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }

    }
}
