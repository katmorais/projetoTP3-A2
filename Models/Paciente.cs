using projetoTP3_A2.Models.Enum;

namespace projetoTP3_A2.Models
{
    public class Paciente : Usuario
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? UltimoLoginEm { get; set; } = DateTime.Now;
        public Perfis Papel { get; set; }
    }
}
