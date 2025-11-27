using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using projetoTP3_A2.Data;
using projetoTP3_A2.Models;

var builder = WebApplication.CreateBuilder(args);

// --------------------------------------------------------
// CONFIGURAÇÃO DO BANCO DE DADOS SQL SERVER
// --------------------------------------------------------
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// --------------------------------------------------------
// IDENTITY COM ROLES
// --------------------------------------------------------
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole<Guid>>() // habilita roles com chave Guid
.AddEntityFrameworkStores<ApplicationDbContext>();

// --------------------------------------------------------
// MVC + CONTROLLERS + VIEWS
// --------------------------------------------------------
builder.Services.AddControllersWithViews();

// Razor Pages (mantido para login/registro do Identity)
builder.Services.AddRazorPages();

var app = builder.Build();

// --------------------------------------------------------
// PIPELINE DE REQUISIÇÃO
// --------------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// --------------------------------------------------------
// ROTAS MVC
// --------------------------------------------------------
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// --------------------------------------------------------
// SEED DE ROLES
// --------------------------------------------------------
async Task CreateRoles(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

    string[] roleNames = { "Administrador", "Medico", "Farmaceutico", "Paciente" };

    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
        }
    }
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await CreateRoles(services);
}

// Razor Pages (necessário para Identity)
app.MapRazorPages();

app.Run();