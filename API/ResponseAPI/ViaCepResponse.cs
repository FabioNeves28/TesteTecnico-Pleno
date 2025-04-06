using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClienteService.API.ResponseAPI
{
    public class ViaCepResponse
    {
        [JsonProperty("logradouro")]
        public string Logradouro { get; set; }

        [JsonProperty("bairro")]
        public string Bairro { get; set; }

        [JsonProperty("localidade")]
        public string Localidade { get; set; }

        [JsonProperty("uf")]
        public string Uf { get; set; }

        [JsonProperty("erro")]
        public bool? Erro { get; set; }
    }

}
