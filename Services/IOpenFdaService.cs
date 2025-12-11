namespace projetoTP3_A2.Services.Interfaces
{
    public interface IOpenFdaService
    {
        Task<string> BuscarInteracoesBulaAsync(string nomeMedicamento);
        
        Task<bool> VerificarInteracaoAsync(string medicamentoNovo, string medicamentoEmUso);
    }
}