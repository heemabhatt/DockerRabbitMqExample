using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DockerRabbitMqExample.Mq;
using DockerRabbitMqExample.Models;
using RabbitMQ.Client;
using System;

namespace DockerRabbitMqExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IConnection Connection { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            IConfigurationSection sec = Configuration.GetSection("MqSettings");
            services.Configure<MqSettings>(sec);
            services.AddSingleton(Configuration);

            IConnectionFactory connectionFactory;
            if (Convert.ToBoolean(Configuration.GetSection("MqSettings:IsLocal").Value))
            {
                 connectionFactory = new RabbitMQ.Client.ConnectionFactory()
                {
                    UserName = Configuration.GetSection("MqSettings:UserName").Value,
                    Password = Configuration.GetSection("MqSettings:Password").Value,
                    HostName = Configuration.GetSection("MqSettings:HostName").Value,
                };
            }
            else
            {
                connectionFactory = new RabbitMQ.Client.ConnectionFactory()
                {
                    Uri = new System.Uri("amqp://iahqjqiq:NYP7vJoZSAVh37yVBj5D3iDVg5nEx3_r@termite.rmq.cloudamqp.com/iahqjqiq"),
                };
            }


            var connection = connectionFactory.CreateConnection();
            services.AddSingleton<IConnection>(connection);
            services.AddSingleton<IModelService, ModelService>();
            services.AddSingleton<IMqService, MqService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
