using Newtonsoft.Json;
using RabbitMQ.Client;

var factory = new ConnectionFactory
{
    // URI_Pattern = protocol://username:password@server:port
    Uri = new Uri("amqp://guest:guest@localhost:5672")
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
channel.QueueDeclare("demo-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);

var message = new { Name = "Producer", Message = "Hello!" };

// Convert message to bit array object.
var body = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

channel.BasicPublish("","demo-queue", null, body);

