using System.Text.Json;
using projetoTP3_A2.Dtos;
using projetoTP3_A2.Services.Interfaces;

namespace projetoTP3_A2.Services
{
    public class ViaCepService : IViaCepService
    {
        private readonly HttpClient _httpClient;

        public ViaCepService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ViaCepResponseDto> BuscarEnderecoPorCepAsync(string cep)
        {
            var response = await _httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            var endereco = JsonSerializer.Deserialize<ViaCepResponseDto>(content);

            if (endereco != null && endereco.Erro)
            {
                return null;
            }

            return endereco;
        }
    }
}