using LenteVivo.Data;
using LenteVivo.Models;
using LenteVivo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LenteVivo.Controllers;

public class CotizadorController : Controller
{
    private readonly ICotizadorService _cotizador;
    private readonly LenteVivoContext _context;

    public CotizadorController(ICotizadorService cotizador, LenteVivoContext context)
    {
        _cotizador = cotizador;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var model = new CotizadorViewModel();
        await CargarPaquetesAsync(model);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(CotizadorViewModel model)
    {
        await CargarPaquetesAsync(model);

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var resultado = await _cotizador.CalcularAsync(
            model.PaqueteId!.Value,
            model.Personas,
            model.ExtrasSeleccionados);

        if (resultado is null)
        {
            ModelState.AddModelError(
                nameof(model.PaqueteId),
                "El paquete seleccionado no existe.");

            return View(model);
        }

        model.Resultado = resultado;
        return View(model);
    }

    private async Task CargarPaquetesAsync(CotizadorViewModel model)
    {
        model.Paquetes = await _context.Paquetes
            .AsNoTracking()
            .OrderBy(p => p.Precio)
            .ToListAsync();
    }
}
