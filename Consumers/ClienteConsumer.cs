using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ClienteService.Services
{
    public class ClienteConsumer
    {
        public static async Task ConsumirAsync()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            await using var connection = await factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: "clientes_queue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($" [x] Mensagem recebida: {message}");
                await Task.CompletedTask;
            };

            await channel.BasicConsumeAsync(
                queue: "clientes_queue",
                autoAck: true,
                consumer: consumer
            );

            Console.WriteLine(" Pressione [enter] para sair.");
            Console.ReadLine();
        }
    }
}
