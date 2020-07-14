using DockerRabbitMqExample.Models;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;

namespace DockerRabbitMqExample.Mq
{
    public class MqService : IMqService 
    {
        private readonly IConnectionService _connectionService;
        private readonly IOptions<MqSettings> _mqSettings;
         
        public MqService(IConnectionService connectionService, IOptions<MqSettings> mqSettings)
        {
            _connectionService = connectionService;
            _mqSettings = mqSettings;
        }

        public void PublishToMq(Message message)
        {
            // Main entry point to the RabbitMQ .NET AMQP client
            var model = _connectionService.GetModel();

            var properties = model.CreateBasicProperties();
            properties.Persistent = false;
            
            // Serialize Message
            var serializedMessage = Newtonsoft.Json.JsonConvert.SerializeObject(message);
            byte[] messagebuffer = Encoding.Default.GetBytes(serializedMessage);

            // Publish Message
            model.BasicPublish( _mqSettings.Value.ExchangeSettings.ExchangeName, _mqSettings.Value.QueueSettings.RoutingKey, properties, messagebuffer);
        }
    }  
}