using System.Text.Json;
using projetoTP3_A2.Dtos;
using projetoTP3_A2.Services.Interfaces;

namespace projetoTP3_A2.Services
{
    public class OpenFdaService : IOpenFdaService
    {
        private readonly HttpClient _httpClient;

        public OpenFdaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> BuscarInteracoesBulaAsync(string nomeMedicamento)
        {
            var termo = nomeMedicamento.Trim();
            var query = $"search=openfda.brand_name:\"{termo}\"+OR+openfda.generic_name:\"{termo}\"&limit=1";
            
            try 
            {
                var response = await _httpClient.GetAsync($"https://api.fda.gov/drug/label.json?{query}");

                if (!response.IsSuccessStatusCode) return null;

                var content = await response.Content.ReadAsStringAsync();
                var dados = JsonSerializer.Deserialize<OpenFdaResponseDto>(content);

                if (dados?.Results != null && dados.Results.Count != 0)
                {
                    var resultado = dados.Results[0];
                    var textoCompleto = new List<string>();

                    if (resultado.DrugInteractions != null)
                    {
                        textoCompleto.AddRange(resultado.DrugInteractions);
                    }

                    if (resultado.Warnings != null)
                    {
                        textoCompleto.AddRange(resultado.Warnings);
                    }

                    return string.Join(" ", textoCompleto);
                }
            }
            catch
            {
                return null;
            }

            return null;
        }

        public async Task<bool> VerificarInteracaoAsync(string medicamentoNovo, string medicamentoEmUso)
        {
            var textoInteracoes = await BuscarInteracoesBulaAsync(medicamentoNovo);

            if (string.IsNullOrEmpty(textoInteracoes)) return false;
            
            if (textoInteracoes.Contains(medicamentoEmUso, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            return false;
        }
    }
}