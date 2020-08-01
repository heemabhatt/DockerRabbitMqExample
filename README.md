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
docker login
docker push heemadocker/dockerrabbitmqexample:v1

docker ps

docker exec -it [container_id] bash


# Run on Linux VM on Azure
https://dzone.com/articles/how-to-install-docker-on-ubuntu1804
Allow port 7001
