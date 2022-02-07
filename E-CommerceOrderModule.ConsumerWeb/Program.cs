using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace E_CommerceOrderModule.ConsumerWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog(configureLogger: (context, configuration) =>
             {
                 configuration.Enrich.FromLogContext()
                 .Enrich.WithMachineName()
                 //.WriteTo.Console()
                 .WriteTo.Elasticsearch(
                     new ElasticsearchSinkOptions(node: new Uri(context.Configuration["ElasticConfiguration:Uri"]))
                     {
                         IndexFormat = $"{context.Configuration["ApplicationName"]}-logs-{context.HostingEnvironment.EnvironmentName?.ToLower().Replace(oldValue: ".", newValue: "-")}-{DateTime.UtcNow:yyyy-MM}",
                         AutoRegisterTemplate = true,
                         NumberOfShards = 2,
                         NumberOfReplicas = 1
                     })
                 .Enrich.WithProperty(name: "Environment", context.HostingEnvironment.EnvironmentName)
                 .ReadFrom.Configuration(context.Configuration);
             })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
