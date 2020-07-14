# DockerRabbitMqExample
POC for .Net Core APIs to post payload to RabbitMq. and Docker as deployment unit. 

# Useful resources

https://www.tutlane.com/tutorial/rabbitmq/csharp-publish-message-to-rabbitmq-queue

https://docs.docker.com/engine/examples/dotnetcore/

# Commands

dotnet build

dotnet run

docker build -t heemadocker/dockerrabbitmqexample:v1 .

docker run -it -p 7001:80 heemadocker/dockerrabbitmqexample:v1

docker push heemadocker/dockerrabbitmqexample:v1

docker ps

docker exec -it [container_id] bash
