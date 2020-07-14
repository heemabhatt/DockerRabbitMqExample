using RabbitMQ.Client;

namespace DockerRabbitMqExample.Mq
{
    public interface IConnectionService
    {
         public IModel GetModel() ;
    }
}