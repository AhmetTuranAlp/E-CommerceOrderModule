using E_CommerceOrderModule.ConsumerWorker.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceOrderModule.ConsumerWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                IConfiguration Configuration = hostContext.Configuration;
                services.AddSingleton<RabbitMQClientService>();
                services.AddSingleton(sp => new ConnectionFactory() { Uri = new Uri(Configuration.GetConnectionString("RabbitMQ")) });
                services.AddHostedService<Worker>();
            })
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
            });






    }
}
