using projetoTP3_A2.Models;

namespace projetoTP3_A2.Models.ViewModels
{
    public class ProntuarioViewModel
    {
        public ApplicationUser Paciente { get; set; }
        public List<Prescricao> HistoricoPrescricoes { get; set; } = new();
    }
}