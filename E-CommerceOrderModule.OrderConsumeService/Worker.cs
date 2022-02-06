using E_CommerceOrderModule.Core.DTOs;
using E_CommerceOrderModule.Core.Entity;
using E_CommerceOrderModule.OrderConsumeService.RabbitMQ;
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

namespace E_CommerceOrderModule.OrderConsumeService
{
    public class Worker : BackgroundService
    {
        private readonly RabbitMQClientService _rabbitMQClientService;
        private readonly IServiceProvider _serviceProvider;
        private IModel _channel;

        public Worker(RabbitMQClientService rabbitMQClientService, IServiceProvider serviceProvider)
        {
            _rabbitMQClientService = rabbitMQClientService;
            _serviceProvider = serviceProvider;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _rabbitMQClientService.Connect();
            _channel.BasicQos(0, 1, false); // RabbitMQ tarafından 1 adet olarak gönderilmesi yapılmaktadır.
            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            _channel.BasicConsume(RabbitMQClientService.QueueName, false, consumer);
            consumer.Received += Consumer_Received;
            return Task.CompletedTask;
        }


        private Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            var user = JsonSerializer.Deserialize<UserDTO>(Encoding.UTF8.GetString(@event.Body.ToArray()));
            if (user != null)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ECommerceOrderModuleContext>();

                    var basketList = context.Baskets.ToList();
                    if (basketList.Count > 0)
                    {
                        var baskets = basketList.Where(x => x.UserCode == user.Id.ToString()).ToList();

                        #region Ödeme Modeline Bilgiler Set Ediliyor.
                        Sales sales = new Sales()
                        {
                            Status = ModelEnums.Status.Active,
                            UploadDate = DateTime.Now,
                            UpdateDate = DateTime.Now,

                        };
                        #endregion

                        //var productList = context.Products.ToList();

                        foreach (var x in baskets)
                        {
                            #region Ürün Stok Bilgisi Güncelleniyor.
                            var product = context.Products.Where(c => c.ProductId == x.ProductCode).FirstOrDefault();
                            if (product != null)
                            {
                                product.Stock -= x.Quantity;
                                context.Products.Update(product);
                            }
                            #endregion

                            #region Sepetdeki Ürün Satış İşleminden Dolayı Statusu Silindiye Çekiliyor.
                            x.Status = ModelEnums.Status.Deleted;
                            context.Baskets.Update(x);
                            #endregion

                            #region Ödeme Modeline Bilgiler Set Ediliyor.                 
                            sales.TotalPrice += x.Price * x.Quantity;
                            sales.PaymentType = "Kredi Kartı (Tek Çekim)";
                            sales.TotalQuantity += x.Quantity;
                            sales.UserCode = user.Id.ToString();
                            sales.UserName = user.UserName;
                            #endregion
                        }

                        context.Sales.Add(sales);
                        var status = context.SaveChanges();
                        if (status > 0)
                        {
                            _channel.BasicAck(@event.DeliveryTag, false);
                        }
                    }
                }

            }
            return Task.CompletedTask;
        }




    }
}
