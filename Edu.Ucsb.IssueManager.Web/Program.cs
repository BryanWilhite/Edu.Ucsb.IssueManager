using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Edu.Ucsb.IssueManager.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args, builderAction: null).Build().Run();
        }

       public static IWebHostBuilder CreateWebHostBuilder(string[] args, Action<WebHostBuilderContext, IConfigurationBuilder> builderAction) =>
            WebHost
                .CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((builderContext, configBuilder) =>
                {
                    builderAction?.Invoke(builderContext, configBuilder);
                })
                .UseStartup<Startup>()
                ;
    }
}
