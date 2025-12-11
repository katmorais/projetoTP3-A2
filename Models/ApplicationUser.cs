using Microsoft.AspNetCore.Identity;
using projetoTP3_A2.Models.Enum;

namespace projetoTP3_A2.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Nome { get; set; } = string.Empty;
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
        public DateTime? UltimoLoginEm { get; set; }
        public Perfis Perfil { get; set; }
    }
}