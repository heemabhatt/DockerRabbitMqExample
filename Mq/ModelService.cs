using DockerRabbitMqExample.Models;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace DockerRabbitMqExample.Mq
{
    public class ModelService : IModelService
    {
         // private readonly IConnectionService _connectionService;
         private readonly IConnection _connection;
        private readonly IOptions<MqSettings> _mqSettings;
        public ModelService( IConnection connection, IOptions<MqSettings> mqSettings)
        {
          //  _connectionService = connectionService;
          _connection=connection;
            _mqSettings = mqSettings;
        }

        public IModel GetModel()
        {   
           // var connection = _connectionService.GetConnection();

            var model = _connection.CreateModel();

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