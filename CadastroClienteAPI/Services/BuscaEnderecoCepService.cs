using CadastroClienteAPI.Models;
using CadastroClienteAPI.Models.DTO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace CadastroClienteAPI.Services
{
    public class BuscaEnderecoCepService
    {
        private readonly HttpClient _httpClient;

        public BuscaEnderecoCepService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<EnderecoViaCepDTO> BuscarEnderecoPorCep(string cep)
        {
            if (!Regex.IsMatch(cep, @"^\d{8}$"))
            {
                throw new ArgumentException("O CEP deve conter exatamente 8 números.");
            }

            var url = $"https://viacep.com.br/ws/{cep}/json/";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException("Erro ao buscar o endereço. Verifique o CEP.");
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<EnderecoViaCepDTO>(content);
        }

        public async Task<EnderecoViaCepDTO> BuscaEnderecoPorCepAsync(string cep)
        {
            // URL da API ViaCEP
            var url = $"https://viacep.com.br/ws/{cep}/json/";

            // Faz a requisição GET à API
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            // Lê e deserializa o JSON para o objeto EnderecoViaCepDTO
            var enderecoViaCep = await response.Content.ReadFromJsonAsync<EnderecoViaCepDTO>();

            if (enderecoViaCep == null || enderecoViaCep.Cep == null)
            {
                return null;
            }

            return enderecoViaCep;
        }
    }


}
