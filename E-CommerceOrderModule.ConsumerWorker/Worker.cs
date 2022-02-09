using E_CommerceOrderModule.Common;
using E_CommerceOrderModule.ConsumerWorker.DependencyInjection;
using E_CommerceOrderModule.ConsumerWorker.RabbitMQ;
using E_CommerceOrderModule.Core.Asbtract.Services;
using E_CommerceOrderModule.Core.DTOs;
using E_CommerceOrderModule.Core.Entity;
using E_CommerceOrderModule.Repository.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace E_CommerceOrderModule.ConsumerWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private RabbitMQClientService _rabbitMQClientService;
        private readonly IServiceProvider _serviceProvider;
        private IModel _channel;
        public Worker(ILogger<Worker> logger, RabbitMQClientService rabbitMQClientService, IServiceProvider serviceProvider)
        {
            _rabbitMQClientService = rabbitMQClientService;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _rabbitMQClientService.Connect();
            _channel.BasicQos(0, 1, false); // RabbitMQ tarafından 1 adet olarak gönderilmesi yapılmaktadır.
            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            _channel.QueueDeclare(RabbitMQClientService.QueueName, true, false, false, null);
            _channel.BasicConsume(RabbitMQClientService.QueueName, false, consumer);
            consumer.Received += (model, e) =>
            {
                GetReciver(e);
            };
            return Task.CompletedTask;
        }

        public void GetReciver(BasicDeliverEventArgs @event)
        {
            var basketRequest = JsonSerializer.Deserialize<BasketRequestDTO>(Encoding.UTF8.GetString(@event.Body.ToArray()));
            if (basketRequest != null)
            {
                #region DependencyInjection
                var _serviceProvider = DIOperation.ServiceProviderInjection();
                var _basketService = _serviceProvider.GetService<IBasketService>();
                var _productService = _serviceProvider.GetService<IProductService>();
                var _userService = _serviceProvider.GetService<IUserService>();
                var _saleService = _serviceProvider.GetService<ISaleService>();
                #endregion

                var baskets = _basketService.GetAllSaleAsync(basketRequest.UserCode, basketRequest.BasketId).Result;
                if (baskets.ResultStatus && baskets.ResultObject.Count > 0)
                {
                    #region Ödeme Modeline Bilgiler Set Ediliyor.
                    SalesDTO sales = new SalesDTO()
                    {
                        OrderNumber = basketRequest.BasketId,
                        Status = ModelEnumsDTO.Status.Active,
                        UploadDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        IsLog = true
                    };
                    #endregion

                    var productList = _productService.GetAllProductAsync().Result;

                    foreach (var x in baskets.ResultObject)
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

                        #region Sepetdeki Ürün Satış İşleminden Dolayı Statusu Satış Bitti Çekiliyor.
                        x.Status = ModelEnumsDTO.Status.SaleFinish;
                        _basketService.UpdateBasket(x);
                        #endregion

                        #region Ödeme Modeline Bilgiler Set Ediliyor.                 
                        sales.TotalPrice += x.Price * x.Quantity;

                        if (sales.TotalPrice > 500)
                            sales.TotalPrice += Convert.ToDecimal(14.99);

                        sales.PaymentType = "Kredi Kartı (Tek Çekim)";
                        sales.TotalQuantity += x.Quantity;
                        sales.UserCode = basketRequest.UserCode;

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
                        string log = $"Sipariş Bilgileri: Sipariş Numarası: {sales.OrderNumber} Toplam Fiyat: {sales.TotalPrice} Toplam Adet: {sales.TotalQuantity}";
                        _logger.LogInformation(log);
                        //Console.Clear();
                        Console.WriteLine(log);
                        _channel.BasicAck(@event.DeliveryTag, false);

                    }
                }

            }

        }
    }
}
