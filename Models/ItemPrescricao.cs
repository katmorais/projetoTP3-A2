using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projetoTP3_A2.Models
{
    public class ItemPrescricao
    {
        public Guid Id { get; set; }

        [ForeignKey("Prescricao")]
        public Guid PrescricaoId { get; set; }
        public virtual Prescricao Prescricao { get; set; }

        [ForeignKey("Medicamento")]
        public int MedicamentoId { get; set; }
        public virtual Medicamento Medicamento { get; set; }

        public int Quantidade { get; set; }

        [StringLength(500)]
        public string InstrucoesUso { get; set; }
    }
}