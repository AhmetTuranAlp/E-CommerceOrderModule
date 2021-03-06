using E_CommerceOrderModule.Core.Asbtract;
using E_CommerceOrderModule.Core.Asbtract.Repositories;
using E_CommerceOrderModule.Core.Asbtract.Services;
using E_CommerceOrderModule.Repository.Concrete;
using E_CommerceOrderModule.Repository.Concrete.Repositories;
using E_CommerceOrderModule.Repository.Context;
using E_CommerceOrderModule.Services.Mapping;
using E_CommerceOrderModule.Services.Services;
using E_CommerceOrderModule.Web.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceOrderModule.Web
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
            services.AddRazorPages();
            services.AddMvc();
            services.AddDbContext<ECommerceOrderModuleContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlConnection")));
            services.AddSingleton(sp => new ConnectionFactory() { Uri = new Uri(Configuration.GetConnectionString("RabbitMQ")) });
            services.AddSingleton<RabbitMQClientService>();
            services.AddSingleton<RabbitMQPublisher>();

            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient(typeof(IProductRepository), typeof(ProductRespository));
            services.AddTransient(typeof(IUserRepository), typeof(UserRepository));
            services.AddTransient(typeof(IBasketRepository), typeof(BasketRepository));
            services.AddTransient(typeof(ISaleRepository), typeof(SaleRepository));
            services.AddTransient(typeof(IService<>), typeof(Service<>));
            services.AddTransient(typeof(IProductService), typeof(ProductService));
            services.AddTransient(typeof(IUserService), typeof(UserService));
            services.AddTransient(typeof(IBasketService), typeof(BasketService));
            services.AddTransient(typeof(ISaleService), typeof(SaleService));
            services.AddAutoMapper(typeof(MapProfile));
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=Index}/{id?}");
            });
        }
    }
}
