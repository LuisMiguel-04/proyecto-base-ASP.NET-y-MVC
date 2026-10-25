using LenteVivo.Models;
using LenteVivo.Services;
using Microsoft.AspNetCore.Mvc;

namespace LenteVivo.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        var modelo = new HomeViewModel
        {
            Servicios = Catalogo.Servicios,
            Proceso = Catalogo.Proceso,
            Galeria = Catalogo.Galeria,
            Paquetes = Catalogo.Paquetes
        };

        return View(modelo);
    }

    [HttpGet]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(
        Duration = 0,
        Location = ResponseCacheLocation.None,
        NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}