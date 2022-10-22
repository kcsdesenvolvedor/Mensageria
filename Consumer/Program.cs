// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory() { HostName = "localhost"};

using (var connection = factory.CreateConnection())
using (var chanel = connection.CreateModel())
{
    chanel.QueueDeclare(
            queue: "myQueue",
            durable:false,
            exclusive:false,
            autoDelete:false,
            arguments: null
        );
    var consumer = new EventingBasicConsumer(chanel);
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);

        Console.WriteLine("[x] Recebido - {0}", message);
    };
    chanel.BasicConsume(queue: "myQueue", autoAck: true, consumer: consumer);

    Console.ReadKey();
}
