using RabbitMQ.Client;

namespace DockerRabbitMqExample.Mq
{
    public interface IModelService
    {
        public IModel GetModel();
    }
}