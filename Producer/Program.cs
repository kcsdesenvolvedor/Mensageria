// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory()
{
    HostName = "localhost",
    UserName = "guest",
    Password = "guest"
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

    while (true)
    {
        Console.WriteLine("Deseja enviar uma mensagem? s/n");
        var option = Console.ReadLine();

        if (option == "n")
            break;

        Console.WriteLine("Digite sua mensagem:");
        string? message = Console.ReadLine();
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(
                exchange: "",
                routingKey: "myQueue",
                basicProperties: null,
                body: body
            );
        Console.WriteLine("Mensagem enviada!");
    }
    
    Console.ReadKey();
}
