using LenteVivo.Models;
using LenteVivo.Services;
using LenteVivo.Models;

namespace LenteVivo.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var modelo = new homeViewModel
        {
            Servicios = Catalgo.Servicios,
            Procesos = Catalgo.Procesos,
            Galeria = Catalgo.Galeria,
            Paquetes = Catalgo.Paquetes
        };
        return View(modelo);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View();
}
