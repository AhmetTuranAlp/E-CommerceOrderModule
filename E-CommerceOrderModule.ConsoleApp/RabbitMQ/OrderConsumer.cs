using E_CommerceOrderModule.Common;
using E_CommerceOrderModule.ConsoleApp.DependencyInjection;
using E_CommerceOrderModule.ConsoleApp.Work;
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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_CommerceOrderModule.ConsoleApp.RabbitMQ
{
    public class OrderConsumer :IWork
    {

        private IModel _channel;
        public static string QueueName = "queue-order";

        public void Work()
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri(StaticValue._rabbitMQUrl);
            using (var connection = factory.CreateConnection())
            {
                using (_channel = connection.CreateModel())
                {
                    _channel.QueueDeclare(QueueName, true, false, false, null);
                    _channel.BasicQos(0, 1, false);

                    var consumer = new EventingBasicConsumer(_channel);
                    _channel.BasicConsume(QueueName, false, consumer);
                    // Received event'i sürekli listen modunda olacaktır.
                    Console.WriteLine("Kanal Dinleniyor...");
                    consumer.Received += (model, e) =>
                    {
                        var user = JsonSerializer.Deserialize<UserDTO>(Encoding.UTF8.GetString(e.Body.ToArray()));
                        GetReciver(e.DeliveryTag, user);
                    };
                    Console.ReadLine();
                }
            }
        }

        private void GetReciver(ulong tag, UserDTO user)
        {
            if (user != null)
            {
                #region DependencyInjection
                var _serviceProvider = DIOperation.ServiceProviderInjection();
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
                        _channel.BasicAck(tag, false);      
                        Console.WriteLine( $"Sipariş Bilgileri: Sipariş Numarası: {sales.OrderNumber} Toplam Fiyat: {sales.TotalPrice} Toplam Adet: {sales.TotalQuantity}");

                    }
                }

            }
        }


    }
}
