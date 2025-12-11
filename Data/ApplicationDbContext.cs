using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using projetoTP3_A2.Models;

namespace projetoTP3_A2.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options)
    {
        public DbSet<Farmacia> Farmacia { get; set; }
        public DbSet<Medicamento> Medicamento { get; set; }
        public DbSet<Patologia> Patologia { get; set; }
        public DbSet<Alergia> Alergia { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Prescricao> Prescricoes { get; set; }
        public DbSet<ItemPrescricao> ItensPrescricao { get; set; }
        public DbSet<Exame> Exames { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Farmacia>(entity =>
            {
                entity.ToTable("Farmacia");
                entity.HasKey(f => f.Id);
                entity.Property(f => f.Nome).IsRequired().HasMaxLength(150);
                entity.Property(f => f.Cep).IsRequired().HasMaxLength(8);
                entity.Property(f => f.Logradouro).HasMaxLength(150);
                entity.Property(f => f.Numero).HasMaxLength(20);
                entity.Property(f => f.Complemento).HasMaxLength(50);
                entity.Property(f => f.Bairro).HasMaxLength(100);
                entity.Property(f => f.Localidade).HasMaxLength(100);
                entity.Property(f => f.Uf).HasMaxLength(2);
            });

            builder.Entity<Prescricao>(entity =>
            {
            entity.HasOne(p => p.Medico)
                .WithMany()
                .HasForeignKey(p => p.MedicoId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.Paciente)
                .WithMany()
                .HasForeignKey(p => p.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Exame>(entity =>
            {
                entity.HasOne(e => e.Medico)
                    .WithMany()
                    .HasForeignKey(e => e.MedicoId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Paciente)
                    .WithMany()
                    .HasForeignKey(e => e.PacienteId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}