using DockerRabbitMqExample.Models;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace DockerRabbitMqExample.Mq
{
    public class ConnectionService : IConnectionService
    {
        private readonly IOptions<MqSettings> _mqSettings;
        public ConnectionService(IOptions<MqSettings> mqSettings)
        {
            _mqSettings = mqSettings;
        }

        public IModel GetModel()
        {
            var connectionFactory = new RabbitMQ.Client.ConnectionFactory()
            {
                UserName = _mqSettings.Value.UserName,
                Password = _mqSettings.Value.Password,
                HostName = _mqSettings.Value.HostName
            };

            var connection = connectionFactory.CreateConnection();

            var model = connection.CreateModel();
            
            // Create Exchange
            model.ExchangeDeclare(_mqSettings.Value.ExchangeSettings.ExchangeName, ExchangeType.Direct);
         
            // Create Queue
            model.QueueDeclare(_mqSettings.Value.QueueSettings.QueueName, true, false, false, null);

            // Bind Queue to Exchange
            model.QueueBind(_mqSettings.Value.QueueSettings.QueueName, _mqSettings.Value.ExchangeSettings.ExchangeName, _mqSettings.Value.QueueSettings.RoutingKey);

            return model;
        }
    }
}