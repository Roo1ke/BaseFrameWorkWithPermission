using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dgssj.WeiXin.Model
{
    public class SysConfig
    {
        public static string DefaultConnection = "";
        public static string DefaultPassword = "";
        public static string Key = "";
        //public static IConfiguration Configuration { get; set; }
        static SysConfig()
        {
            var builder = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            var configurations = configuration.GetSection("SystemConfig");
            DefaultConnection = configurations["DefaultConnection"];
            DefaultPassword = configurations["DefaultPwd"];
            Key = configurations["Key"];
        }
    }
}
