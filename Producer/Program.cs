// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory()
{
    HostName = "localhost",
};

using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(
            queue: "myQueue",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

    string message = "Minha primeira mensagem!";
    var body = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(
            exchange: "",
            routingKey: "myQueue",
            basicProperties: null,
            body: body
        );
    Console.ReadKey();
}
