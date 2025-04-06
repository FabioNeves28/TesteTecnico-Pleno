using ClienteService.Application.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Threading.Tasks;

namespace ClienteService.Infrastructure.Messaging.Services
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly ConnectionFactory _factory;

        public RabbitMQService()
        {
            _factory = new ConnectionFactory { HostName = "localhost" };
        }

        public async Task PublicarMensagemAsync(string mensagem)
        {
            await using var connection = await _factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: "clientes_queue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var body = Encoding.UTF8.GetBytes(mensagem);

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: "clientes_queue",
                body: body
            );
        }
    }
}
