using DockerRabbitMqExample.Models;
using RabbitMQ.Client;
using System.Text;

namespace DockerRabbitMqExample.Mq
{
    public class MqService : IMqService
    {
        public MqService()
        {

        }
        public void PublishToMq(Message message)
        {
             string UserName = "guest";
            string Password = "guest";
            string HostName = "localhost";

            //Main entry point to the RabbitMQ .NET AMQP client

            var connectionFactory = new RabbitMQ.Client.ConnectionFactory()
            {
                UserName = UserName,
                Password = Password,
                HostName = HostName
            };

            var connection = connectionFactory.CreateConnection();

            var model = connection.CreateModel();
              var properties = model.CreateBasicProperties();

            properties.Persistent = false;
          // Create Exchange

            model.ExchangeDeclare("demoExchange", ExchangeType.Direct);
         
            // Create Queue

            model.QueueDeclare("demoqueue", true, false, false, null);

            // Bind Queue to Exchange
            model.QueueBind("demoqueue", "demoExchange", "directexchange_key");
            
            var serializedMessage = Newtonsoft.Json.JsonConvert.SerializeObject(message);

            byte[] messagebuffer = Encoding.Default.GetBytes(serializedMessage);
            model.BasicPublish("demoExchange", "directexchange_key", properties, messagebuffer);
        }
    }  
}