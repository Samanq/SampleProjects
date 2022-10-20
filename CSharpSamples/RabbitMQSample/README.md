# RabbitMQ Sample

## Installing RabbitMQ on Docker
1. Open Terminal and run this command
```powershell
docker run -d --hostname my-rabbit --name ecomm-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:3-management
```
2. Now you can browse the adminPanle throgh http://localhost:15672
    
    The default credential is:

    Username: guest

    Password: guest

3. Cerate a Console application and name it RabbitMQSample.Producer
4. Install RabbitMQ.Client Packge From NuGet
