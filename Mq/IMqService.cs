using DockerRabbitMqExample.Models;

namespace DockerRabbitMqExample.Mq
{
    public interface IMqService
    {
        public void PublishToMq(Message message);
    }  
}