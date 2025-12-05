using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace projetoTP3_A2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        [Authorize(Policy = "AdministradorPolicy")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Página Inicial"; // garante que não será null
            return View();
        }

        [Authorize(Policy = "AdministradorPolicy")]
        public IActionResult MedicamentoHome()
        {
            ViewData["Title"] = "Área do médico"; // garante que não será null
            return View();
        }

        [Authorize(Policy = "FarmaceuticoPolicy")]
        public IActionResult FarmaceuticoHome()
        {
            ViewData["Title"] = "Área do Farmacêutico";
            return View(); // procura Views/Home/FarmaceuticoHome.cshtml
        }

        [Authorize(Policy = "MedicoPolicy")]
        public IActionResult MedicoHome()
        {
            ViewData["Title"] = "Área do Médico";
            return View(); // procura Views/Home/MedicoHome.cshtml
        }

        [Authorize(Policy = "PacientePolicy")]
        public IActionResult PacienteHome()
        {
            ViewData["Title"] = "Área do Paciente";
            return View(); // procura Views/Home/PacienteHome.cshtml
        }

    }
}