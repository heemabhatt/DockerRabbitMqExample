using Microsoft.AspNetCore.Mvc;
using DockerRabbitMqExample.Models;
using DockerRabbitMqExample.Mq;

namespace DockerRabbitMqExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MqController : ControllerBase
    {
        private readonly IMqService _mqService;
        public MqController(IMqService mqService)
        {
            _mqService=mqService;
        }

        [HttpPost]
        public void Post([FromBody]Message message)
        {   
            _mqService.PublishToMq(message);
        }

        [HttpGet]
        public string Get()
        {
            return "Hello world";
        }
    }
}