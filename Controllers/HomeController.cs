using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace projetoTP3_A2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [Authorize(Roles = "Administrador")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Área do Administrador";
            return View();
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult MedicamentoHome()
        {
            ViewData["Title"] = "Gestão de Medicamentos";
            return View();
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult AlergiaHome()
        {
            ViewData["Title"] = "Gestão de Alergias";
            return View();
        }

        [Authorize(Roles = "Farmaceutico,Administrador")]
        public IActionResult FarmaceuticoHome()
        {
            ViewData["Title"] = "Área do Farmacêutico";
            return View();
        }

        [Authorize(Roles = "Medico,Administrador")]
        public IActionResult MedicoHome()
        {
            ViewData["Title"] = "Área do Médico";
            return View();
        }

        [Authorize(Roles = "Paciente,Administrador")]
        public IActionResult PacienteHome()
        {
            ViewData["Title"] = "Área do Paciente";
            return View();
        }
    }
}