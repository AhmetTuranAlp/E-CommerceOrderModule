using E_CommerceOrderModule.OrderConsumeService.RabbitMQ;
using E_CommerceOrderModule.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceOrderModule.OrderConsumeService
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
                    services.AddDbContext<ECommerceOrderModuleContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlConnection")));

                    services.AddSingleton<RabbitMQClientService>();
                    services.AddSingleton(sp => new ConnectionFactory() { Uri = new Uri(Configuration.GetConnectionString("RabbitMQ")), DispatchConsumersAsync = true });

                    services.AddHostedService<Worker>();
                });
    }
}
