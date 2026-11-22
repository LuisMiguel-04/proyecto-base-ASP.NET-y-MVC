using LenteVivo.Data;
using LenteVivo.Models;
using LenteVivo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LenteVivo.Controllers;

public class HomeController : Controller
{
    private readonly LenteVivoContext _context;

    public HomeController(LenteVivoContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var modelo = new HomeViewModel
        {
            Servicios = Catalogo.Servicios,
            Proceso = Catalogo.Proceso,
            Galeria = Catalogo.Galeria,
            Paquetes = await _context.Paquetes
                .AsNoTracking()
                .OrderBy(p => p.Precio)
                .ToListAsync()
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
