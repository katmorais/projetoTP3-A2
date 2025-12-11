using System.Text.Json.Serialization;

namespace projetoTP3_A2.Dtos
{
    public class OpenFdaResponseDto
    {
        [JsonPropertyName("results")]
        public List<OpenFdaResultDto> Results { get; set; }
    }

    public class OpenFdaResultDto
    {
        // Para remédios controlados
        [JsonPropertyName("drug_interactions")]
        public List<string> DrugInteractions { get; set; }

        // ADICIONE ISTO: Para remédios OTC (como Aspirina)
        [JsonPropertyName("warnings")]
        public List<string> Warnings { get; set; }

        [JsonPropertyName("openfda")]
        public OpenFdaMetadataDto OpenFda { get; set; }
    }

    public class OpenFdaMetadataDto
    {
        [JsonPropertyName("brand_name")]
        public List<string> BrandName { get; set; }

        [JsonPropertyName("generic_name")]
        public List<string> GenericName { get; set; }
    }
}