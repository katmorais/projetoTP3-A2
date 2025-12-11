using System.ComponentModel.DataAnnotations;

namespace projetoTP3_A2.Models.ViewModels
{
    public class PrescricaoViewModel
    {
        [Required(ErrorMessage = "Selecione um paciente")]
        public Guid PacienteId { get; set; }

        [Required(ErrorMessage = "Selecione um medicamento")]
        public int MedicamentoId { get; set; }

        [Required(ErrorMessage = "Informe a quantidade")]
        [Range(1, 100, ErrorMessage = "Quantidade deve ser entre 1 e 100")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "Instruções são obrigatórias")]
        public string Instrucoes { get; set; }
        
        // Campo apenas para exibir avisos de interação sem bloquear, se quiser
        public string? AlertaInteracao { get; set; }
    }
}