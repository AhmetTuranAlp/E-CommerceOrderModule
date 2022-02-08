using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceOrderModule.Web.RabbitMQ
{
    public class RabbitMQClientService : IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        public static string ExchangeName = "OrderDirectExchange";
        public static string RoutingOrder = "order-route";
        public static string QueueName = "queue-order";

        //DI üzerinden gelen nesne özeliği alınmaktadır.
        //Connect metodu nesne örneği alındıgında çağrılmaktadır.
        public RabbitMQClientService(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            Connect();
        }

        //Bağlantı kurma
        public IModel Connect()
        {
            _connection = _connectionFactory.CreateConnection();
            if (_channel is { IsOpen: true })
            {
                return _channel;
            }

            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(ExchangeName, type: "direct", true, false);
            //Kuyruk oluşturma işlemi yapılmaktadır.
            _channel.QueueDeclare(QueueName, true, false, false, null);
            //Bind işlemi yapılmaktadır.
            _channel.QueueBind(exchange: ExchangeName, queue: QueueName, routingKey: RoutingOrder);

            return _channel;
        }

        public void Dispose()
        {
            _channel?.Close();//var ise kapatılmaktadır. 
            _channel?.Dispose();//var ise disponse edilmektedir. 

            _connection?.Close(); //var ise kapatılmaktadır. 
            _connection?.Dispose();//var ise disponse edilmektedir. 
        }
    }
}
