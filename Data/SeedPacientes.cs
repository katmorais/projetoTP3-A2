using Microsoft.AspNetCore.Identity;
using projetoTP3_A2.Models;
using projetoTP3_A2.Models.Enum;

namespace projetoTP3_A2.Data
{
    public static class SeedData
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            await CriarRoleSeNaoExistir(roleManager, "Administrador");
            await CriarRoleSeNaoExistir(roleManager, "Medico");
            await CriarRoleSeNaoExistir(roleManager, "Farmaceutico");
            await CriarRoleSeNaoExistir(roleManager, "Paciente");

            string senhaPadrao = "12345678";

            if (await userManager.FindByEmailAsync("admin@susdigital.com") == null)
            {
                var admin = new ApplicationUser
                {
                    UserName = "admin@susdigital.com",
                    Email = "admin@susdigital.com",
                    Nome = "Administrador do Sistema",
                    Perfil = Perfis.Administrador,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(admin, senhaPadrao);
                await userManager.AddToRoleAsync(admin, "Administrador");
            }

            if (await userManager.FindByEmailAsync("medico@susdigital.com") == null)
            {
                var medico = new Medico
                {
                    UserName = "medico@susdigital.com",
                    Email = "medico@susdigital.com",
                    Nome = "Dr. Roberto House",
                    Perfil = Perfis.Medico,
                    CRM = "12345-SP",
                    Especialidade = "Clínica Geral",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(medico, senhaPadrao);
                await userManager.AddToRoleAsync(medico, "Medico");
            }

            if (await userManager.FindByEmailAsync("farma@susdigital.com") == null)
            {
                var farma = new ApplicationUser
                {
                    UserName = "farma@susdigital.com",
                    Email = "farma@susdigital.com",
                    Nome = "Ana Paula Farmacêutica",
                    Perfil = Perfis.Farmaceutico,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(farma, senhaPadrao);
                await userManager.AddToRoleAsync(farma, "Farmaceutico");
            }

            var pacientes = new List<ApplicationUser>
            {
                new() { UserName = "joao.silva@teste.com", Email = "joao.silva@teste.com", Nome = "João Silva", Perfil = Perfis.Paciente, EmailConfirmed = true },
                new() { UserName = "maria.souza@teste.com", Email = "maria.souza@teste.com", Nome = "Maria Souza", Perfil = Perfis.Paciente, EmailConfirmed = true },
                new() { UserName = "carlos.pereira@teste.com", Email = "carlos.pereira@teste.com", Nome = "Carlos Pereira", Perfil = Perfis.Paciente, EmailConfirmed = true },
                new() { UserName = "ana.lima@teste.com", Email = "ana.lima@teste.com", Nome = "Ana Lima", Perfil = Perfis.Paciente, EmailConfirmed = true },
                new() { UserName = "paulo.mendes@teste.com", Email = "paulo.mendes@teste.com", Nome = "Paulo Mendes", Perfil = Perfis.Paciente, EmailConfirmed = true },
                new() { UserName = "fernanda.alves@teste.com", Email = "fernanda.alves@teste.com", Nome = "Fernanda Alves", Perfil = Perfis.Paciente, EmailConfirmed = true },
                new() { UserName = "ricardo.gomes@teste.com", Email = "ricardo.gomes@teste.com", Nome = "Ricardo Gomes", Perfil = Perfis.Paciente, EmailConfirmed = true },
                new() { UserName = "juliana.rocha@teste.com", Email = "juliana.rocha@teste.com", Nome = "Juliana Rocha", Perfil = Perfis.Paciente, EmailConfirmed = true },
                new() { UserName = "marcos.santos@teste.com", Email = "marcos.santos@teste.com", Nome = "Marcos Santos", Perfil = Perfis.Paciente, EmailConfirmed = true },
                new() { UserName = "patricia.martins@teste.com", Email = "patricia.martins@teste.com", Nome = "Patrícia Martins", Perfil = Perfis.Paciente, EmailConfirmed = true },
                new() { UserName = "roberto.costa@teste.com", Email = "roberto.costa@teste.com", Nome = "Roberto Costa", Perfil = Perfis.Paciente, EmailConfirmed = true },
                new() { UserName = "camila.ferreira@teste.com", Email = "camila.ferreira@teste.com", Nome = "Camila Ferreira", Perfil = Perfis.Paciente, EmailConfirmed = true },
                new() { UserName = "lucas.araujo@teste.com", Email = "lucas.araujo@teste.com", Nome = "Lucas Araújo", Perfil = Perfis.Paciente, EmailConfirmed = true },
                new() { UserName = "beatriz.monteiro@teste.com", Email = "beatriz.monteiro@teste.com", Nome = "Beatriz Monteiro", Perfil = Perfis.Paciente, EmailConfirmed = true },
                new() { UserName = "felipe.ramos@teste.com", Email = "felipe.ramos@teste.com", Nome = "Felipe Ramos", Perfil = Perfis.Paciente, EmailConfirmed = true },
                new() { UserName = "sandra.barros@teste.com", Email = "sandra.barros@teste.com", Nome = "Sandra Barros", Perfil = Perfis.Paciente, EmailConfirmed = true },
                new() { UserName = "thiago.cardoso@teste.com", Email = "thiago.cardoso@teste.com", Nome = "Thiago Cardoso", Perfil = Perfis.Paciente, EmailConfirmed = true },
                new() { UserName = "aline.machado@teste.com", Email = "aline.machado@teste.com", Nome = "Aline Machado", Perfil = Perfis.Paciente, EmailConfirmed = true },
                new() { UserName = "gustavo.teixeira@teste.com", Email = "gustavo.teixeira@teste.com", Nome = "Gustavo Teixeira", Perfil = Perfis.Paciente, EmailConfirmed = true },
                new() { UserName = "renata.pinto@teste.com", Email = "renata.pinto@teste.com", Nome = "Renata Pinto", Perfil = Perfis.Paciente, EmailConfirmed = true },
                new() { UserName = "eduardo.nunes@teste.com", Email = "eduardo.nunes@teste.com", Nome = "Eduardo Nunes", Perfil = Perfis.Paciente, EmailConfirmed = true },
                new() { UserName = "carolina.dias@teste.com", Email = "carolina.dias@teste.com", Nome = "Carolina Dias", Perfil = Perfis.Paciente, EmailConfirmed = true },
            };

            foreach (var paciente in pacientes)
            {
                if (await userManager.FindByEmailAsync(paciente.Email) == null)
                {
                    await userManager.CreateAsync(paciente, senhaPadrao);
                    await userManager.AddToRoleAsync(paciente, "Paciente");
                }
            }
        }

        private static async Task CriarRoleSeNaoExistir(RoleManager<IdentityRole<Guid>> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
            }
        }
    }
}