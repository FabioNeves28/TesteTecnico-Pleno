﻿using ClienteService.API.ResponseAPI;
using ClienteService.Application.Interfaces;
using ClienteService.Domain.Models;
using ClienteService.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClienteService.API.Controllers
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
            try
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

                await _rabbitMqService.PublicarMensagemAsync($"Novo cliente cadastrado: {cliente.Email}");

                return CreatedAtAction(nameof(CriarCliente), new { id = cliente.Id }, cliente);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.");
            }
        }


        private async Task<ViaCepResponse> BuscarEndereco(string cep)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<ViaCepResponse>($"https://viacep.com.br/ws/{cep}/json/");
            return response?.Erro == true ? null : response;
        }
    }

}
