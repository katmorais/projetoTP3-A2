using System.ComponentModel.DataAnnotations.Schema;
using projetoTP3_A2.Models.Enum;
using projetoTP3_A2.Models.Enum;

namespace projetoTP3_A2.Models
{
    public class Prescricao
    {
        public Guid Id { get; set; }

        public DateTime DataPrescricao { get; set; } = DateTime.UtcNow;

        public DateTime DataValidade { get; set; }

        public PrescricaoStatus Status { get; set; }

        [ForeignKey("Medico")]
        public Guid MedicoId { get; set; }
        public virtual ApplicationUser Medico { get; set; }

        [ForeignKey("Paciente")]
        public Guid PacienteId { get; set; }
        public virtual ApplicationUser Paciente { get; set; }

        public List<ItemPrescricao> Itens { get; set; } = new();
    }
}