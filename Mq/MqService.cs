using DockerRabbitMqExample.Models;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Text;

namespace DockerRabbitMqExample.Mq
{
    public class MqService : IMqService 
    {
        private readonly IModelService _modelService;
        private readonly IOptions<MqSettings> _mqSettings;
         
        public MqService(IModelService modelService, IOptions<MqSettings> mqSettings)
        {
            _modelService = modelService;
            _mqSettings = mqSettings;
        }

        public void PublishToMq(Message message)
        {
            // Serialize Message
            var serializedMessage = Newtonsoft.Json.JsonConvert.SerializeObject(message);
            byte[] messagebuffer = Encoding.Default.GetBytes(serializedMessage);

            // Create Channel
            var model = _modelService.GetModel();

            try{
                var properties = model.CreateBasicProperties();
                properties.Persistent = false;

                if(model.IsOpen)
                {
                    // Publish Message
                   model.BasicPublish( _mqSettings.Value.ExchangeSettings.ExchangeName, _mqSettings.Value.QueueSettings.RoutingKey, properties, messagebuffer); 
                    Console.WriteLine($"Message published on channel {model.ChannelNumber}");
                }
                else
                {
                    Console.WriteLine($"************Message not published because of close connection. {model.ChannelNumber}");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception occurred. {ex.Message}");
                throw ex;
            }
            finally
            {
                model.Dispose();
            }
        }
    }  
}