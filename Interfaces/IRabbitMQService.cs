using Microsoft.AspNetCore.Mvc;

namespace ClienteService.Services
{
    public interface IRabbitMQService
    {
        Task PublicarMensagemAsync(string mensagem);
    }
}
