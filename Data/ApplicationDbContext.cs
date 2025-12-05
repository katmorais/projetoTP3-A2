using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using projetoTP3_A2.Models;

namespace projetoTP3_A2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        internal object Medicamentos;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Farmacia> Farmacia { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Exemplo de configuração adicional (se quiser)
            builder.Entity<Farmacia>(entity =>
            {
                entity.ToTable("Farmacia"); // força plural no nome da tabela
                entity.HasKey(f => f.Id);
                entity.Property(f => f.Nome).IsRequired().HasMaxLength(150);
                entity.Property(f => f.Endereco).HasMaxLength(250);
            });
        }
        public DbSet<projetoTP3_A2.Models.Medicamento> Medicamento { get; set; } = default!;
        public DbSet<projetoTP3_A2.Models.Patologia> Patologia { get; set; } = default!;
    }
}