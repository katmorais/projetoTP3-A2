using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using projetoTP3_A2.Models.Enum;

namespace projetoTP3_A2.Models
{
    public class Exame
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(500)]
        public string Justificativa { get; set; } = string.Empty;

        public string ResultadoUrl { get; set; } = string.Empty;

        public DateTime DataSolicitacao { get; set; } = DateTime.UtcNow;

        public DateTime? DataRealizacao { get; set; }

        public ExameStatus Status { get; set; }

        [ForeignKey("Medico")]
        public Guid MedicoId { get; set; }
        public virtual Medico Medico { get; set; } = null!;
 
        [ForeignKey("Paciente")]
        public Guid PacienteId { get; set; }
        public virtual Paciente Paciente { get; set; } = null!;
    }
}