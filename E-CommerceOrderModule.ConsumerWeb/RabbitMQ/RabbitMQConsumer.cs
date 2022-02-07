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
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_CommerceOrderModule.ConsumerWeb.RabbitMQ
{
    public class RabbitMQConsumer
    {
        private readonly RabbitMQClientService _rabbitMQClientService;
        private IModel _channel;
        private readonly ILogger<RabbitMQConsumer> _logger;
        public static string QueueName = "queue-order";
        public RabbitMQConsumer(RabbitMQClientService rabbitMQClientService,ILogger<RabbitMQConsumer> logger)
        {
            _rabbitMQClientService = rabbitMQClientService;
               _logger = logger;
        }

        public void Consumer()
        {
            _channel = _rabbitMQClientService.Connect();
            _channel.BasicQos(0, 1, false); // RabbitMQ tarafından 1 adet olarak gönderilmesi yapılmaktadır.
            var consumer = new AsyncEventingBasicConsumer(_channel);
            _channel.QueueDeclare(QueueName, true, false, false, null);
            _channel.BasicConsume(QueueName, false, consumer);
            consumer.Received += Consumer_Received;


        }

        private Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            var user = JsonSerializer.Deserialize<UserDTO>(Encoding.UTF8.GetString(@event.Body.ToArray()));
            if (user != null)
            {
                #region DependencyInjection
                var _serviceProvider = ServiceProviderInjection();
                var _basketService = _serviceProvider.GetService<IBasketService>();
                var _productService = _serviceProvider.GetService<IProductService>();
                var _userService = _serviceProvider.GetService<IUserService>();
                var _saleService = _serviceProvider.GetService<ISaleService>();
                #endregion

                var basketList = _basketService.GetAllBasketAsync().Result;
                if (basketList.ResultStatus && basketList.ResultObject.Count > 0)
                {
                    var baskets = basketList.ResultObject.Where(x => x.UserCode == user.Id.ToString()).ToList();

                    #region Ödeme Modeline Bilgiler Set Ediliyor.
                    SalesDTO sales = new SalesDTO()
                    {
                        OrderNumber = Operations.UniqueRandom(1, 9, 10),
                        Status = ModelEnumsDTO.Status.Active,
                        UploadDate = DateTime.Now,
                        UpdateDate = DateTime.Now,

                    };
                    #endregion

                    var productList = _productService.GetAllProductAsync().Result;

                    foreach (var x in baskets)
                    {
                        #region Ürün Stok Bilgisi Güncelleniyor.
                        if (productList.ResultStatus && productList.ResultObject.Count > 0)
                        {
                            var product = productList.ResultObject.Where(c => c.ProductId == x.ProductCode).FirstOrDefault();
                            if (product != null)
                            {
                                product.Stock -= x.Quantity;
                                var res = _productService.UpdateProduct(product).Result;
                            }
                        }
                        #endregion

                        #region Sepetdeki Ürün Satış İşleminden Dolayı Statusu Silindiye Çekiliyor.
                        x.Status = ModelEnumsDTO.Status.Deleted;
                        _basketService.UpdateBasket(x);
                        #endregion

                        #region Ödeme Modeline Bilgiler Set Ediliyor.                 
                        sales.TotalPrice += x.Price * x.Quantity;
                        sales.PaymentType = "Kredi Kartı (Tek Çekim)";
                        sales.TotalQuantity += x.Quantity;
                        sales.UserCode = user.Id.ToString();

                        var userDto = _userService.GetUserAsync().Result;
                        if (userDto.ResultStatus)
                        {
                            sales.UserName = userDto.ResultObject.UserName;
                        }

                        #endregion
                    }

                    var result = _saleService.CreateSales(sales).Result;
                    if (result.ResultStatus)
                    {
                        _logger.LogInformation($"Sipariş Bilgileri: Sipariş Numarası: {sales.OrderNumber} Toplam Fiyat: {sales.TotalPrice} Toplam Adet: {sales.TotalQuantity}");
                        _channel.BasicAck(@event.DeliveryTag, false);
                    }
                }

            }
            return Task.CompletedTask;
        }

        public static ServiceProvider ServiceProviderInjection()
        {
            var serviceDescriptors = new ServiceCollection()
            .AddDbContext<ECommerceOrderModuleContext>(options => options.UseSqlServer(StaticValue._sqlServerConnectionString))
            .AddSingleton(typeof(IUnitOfWork), typeof(UnitOfWork))
            .AddSingleton(typeof(IGenericRepository<>), typeof(GenericRepository<>))
            .AddSingleton(typeof(IProductRepository), typeof(ProductRespository))
            .AddSingleton(typeof(IUserRepository), typeof(UserRepository))
            .AddSingleton(typeof(IBasketRepository), typeof(BasketRepository))
            .AddSingleton(typeof(ISaleRepository), typeof(SaleRepository))
            .AddSingleton(typeof(IService<>), typeof(Service<>))
            .AddSingleton(typeof(IProductService), typeof(ProductService))
            .AddSingleton(typeof(IUserService), typeof(UserService))
            .AddSingleton(typeof(IBasketService), typeof(BasketService))
            .AddSingleton(typeof(ISaleService), typeof(SaleService))
            .AddAutoMapper(typeof(MapProfile))
            .BuildServiceProvider();

            return serviceDescriptors;
        }


    }
}
