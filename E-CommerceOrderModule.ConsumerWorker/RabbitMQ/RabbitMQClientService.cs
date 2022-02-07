using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceOrderModule.ConsumerWorker.RabbitMQ
{
    public class RabbitMQClientService : IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        public static string QueueName = "queue-order";

        private readonly ILogger<RabbitMQClientService> _logger;

        //DI üzerinden gelen nesne özeliği alınmaktadır.
        //Connect metodu nesne örneği alındıgında çağrılmaktadır.
        public RabbitMQClientService(ConnectionFactory connectionFactory, ILogger<RabbitMQClientService> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
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
            _logger.LogInformation("RabbitMQ ile bağlantı kuruldu...");
            return _channel;
        }

        public void Dispose()
        {
            _channel?.Close();//var ise kapatılmaktadır. 
            _channel?.Dispose();//var ise disponse edilmektedir. 

            _connection?.Close(); //var ise kapatılmaktadır. 
            _connection?.Dispose();//var ise disponse edilmektedir. 

            _logger.LogInformation("RabbitMQ ile bağlantı koptu...");
        }
    }
}
