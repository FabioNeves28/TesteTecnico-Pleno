using Microsoft.AspNetCore.Mvc;

namespace ClienteService.Application.Interfaces
{
    public interface IRabbitMQService
    {
        Task PublicarMensagemAsync(string mensagem);
    }
}
