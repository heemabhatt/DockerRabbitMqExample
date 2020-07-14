using Microsoft.AspNetCore.Mvc;
using DockerRabbitMqExample.Models;
using DockerRabbitMqExample.Mq;
using System;

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
        public IActionResult Post([FromBody]Message message)
        {   
            try
            {
                _mqService.PublishToMq(message);
                return Ok();
            }
            catch(Exception ex)
            {
                 Console.WriteLine(ex.Message);
                return StatusCode(500,ex.Message);
            }
        }

        [HttpGet]
        public string Get()
        {
            return "Hello world";
        }
    }
}