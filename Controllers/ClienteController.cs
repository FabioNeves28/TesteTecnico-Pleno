using ClienteService.Data;
using ClienteService.Models;
using ClienteService.ResponseAPI;
using ClienteService.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClienteService.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClientesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IRabbitMQService _rabbitMqService;

        public ClientesController(ApplicationDbContext context, IHttpClientFactory httpClientFactory, IRabbitMQService rabbitMqService)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _rabbitMqService = rabbitMqService;
        }

        [HttpPost]
        public async Task<IActionResult> CriarCliente([FromBody] Cliente cliente)
        {
            if (_context.Clientes.Any(c => c.Email == cliente.Email))
                return BadRequest("Email já cadastrado.");

            var endereco = await BuscarEndereco(cliente.Cep);
            if (endereco == null)
                return BadRequest("CEP inválido.");

            cliente.Logradouro = endereco.Logradouro;
            cliente.Bairro = endereco.Bairro;
            cliente.Cidade = endereco.Localidade;
            cliente.Estado = endereco.Uf;

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            _rabbitMqService.PublicarMensagemAsync($"Novo cliente cadastrado: {cliente.Email}");

            return CreatedAtAction(nameof(CriarCliente), new { id = cliente.Id }, cliente);
        }

        private async Task<ViaCepResponse> BuscarEndereco(string cep)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<ViaCepResponse>($"https://viacep.com.br/ws/{cep}/json/");
            return response?.Erro == true ? null : response;
        }
    }

}
