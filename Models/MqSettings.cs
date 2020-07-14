namespace DockerRabbitMqExample.Models
{
    public class MqSettings
    {
        public string UserName{get;set;}
        public string Password{get;set;}
        public string HostName{get;set;}
        public string MqUri{get;set;}
        public ExchangeSettings ExchangeSettings {get;set;}
        public QueueSettings QueueSettings{get;set;}
        public bool IsLocal{get;set;}
    }

    public class ExchangeSettings
    {
        public string ExchangeName { get; set; }
        public string ExchangeType { get; set; }
    }
    public class QueueSettings
    {
        public string QueueName{get;set;}
        public string RoutingKey{get;set;}
    }
}