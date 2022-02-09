using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceOrderModule.Common;
using E_CommerceOrderModule.Core.Asbtract;
using E_CommerceOrderModule.Core.Asbtract.Repositories;
using E_CommerceOrderModule.Core.Asbtract.Services;
using E_CommerceOrderModule.Core.DTOs;
using E_CommerceOrderModule.Repository.Concrete;
using E_CommerceOrderModule.Repository.Concrete.Repositories;
using E_CommerceOrderModule.Repository.Context;
using E_CommerceOrderModule.Services.Mapping;
using E_CommerceOrderModule.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace E_CommerceOrderModule.ConsumerWorker.DependencyInjection
{
    public class DIOperation
    {
        public static ServiceProvider ServiceProviderInjection()
        {
            var serviceDescriptors = new ServiceCollection()
            .AddDbContext<ECommerceOrderModuleContext>(options => options.UseSqlServer(StaticValue._sqlServerConnectionString))
            .AddSingleton(typeof(IUnitOfWork), typeof(UnitOfWork))
            .AddSingleton(typeof(IGenericRepository<>), typeof(GenericRepository<>))
            .AddSingleton(typeof(IProductRepository), typeof(ProductRespository))
            .AddSingleton(typeof(IOrderProductRepository), typeof(OrderProductRepository))
            .AddSingleton(typeof(IUserRepository), typeof(UserRepository))
            .AddSingleton(typeof(IBasketRepository), typeof(BasketRepository))
            .AddSingleton(typeof(ISaleRepository), typeof(SaleRepository))
            .AddSingleton(typeof(IService<>), typeof(Service<>))
            .AddSingleton(typeof(IProductService), typeof(ProductService))
            .AddSingleton(typeof(IOrderProductService), typeof(OrderProductService))
            .AddSingleton(typeof(IUserService), typeof(UserService))
            .AddSingleton(typeof(IBasketService), typeof(BasketService))
            .AddSingleton(typeof(ISaleService), typeof(SaleService))
            .AddAutoMapper(typeof(MapProfile))
            .BuildServiceProvider();

            return serviceDescriptors;
        }
    }
}
