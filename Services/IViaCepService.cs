using projetoTP3_A2.Dtos;

namespace projetoTP3_A2.Services.Interfaces
{
    public interface IViaCepService
    {
        Task<ViaCepResponseDto> BuscarEnderecoPorCepAsync(string cep);
    }
}