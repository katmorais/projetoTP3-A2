using projetoTP3_A2.Models.Enum;
using System.Xml.Linq;

namespace projetoTP3_A2.Models
{
    public class Paciente : ApplicationUser
    {
        public DateTime DataNascimento { get; set; }
        public Sexo Sexo { get; set; }
        public Raca Raca { get; set; }
        public List<Patologia> Patologias { get; set; } = new();
        public List<Alergia> Alergias { get; set; } = new();
        public Perfis Papel { get; set; }
    }
}
