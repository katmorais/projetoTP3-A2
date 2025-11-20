using projetoTP3_A2.Models.Enum;

namespace projetoTP3_A2.Models
{
    public class Medico : Usuario
    {
        public string CRM { get; set; }
        public string Especialidade { get; set; }
        public Perfis Papel { get; set; }
    }
}
