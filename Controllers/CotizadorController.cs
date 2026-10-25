using LenteVivo.Models;
using LenteVivo.Services;
using Microsoft.AspNetCore.Mvc;

namespace LenteVivo.Controllers;

public class CotizadorController : Controller
{
    private readonly ICotizadorService _cotizador;

    public CotizadorController(ICotizadorService cotizador)
    {
        _cotizador = cotizador;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new CotizadorViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(CotizadorViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var resultado = _cotizador.Calcular(
            model.PaqueteId!,
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
}