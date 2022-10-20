using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory
{
    // URI_Pattern = protocol://username:password@server:port
    Uri = new Uri("amqp://guest:guest@localhost:5672/")
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
channel.QueueDeclare("demo-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, e) => {
    var body = e.Body.ToArray();
    var message = System.Text.Encoding.UTF8.GetString(body);
    Console.WriteLine(message);
};

channel.BasicConsume("demo-queue", true, consumer);

Console.ReadKey();