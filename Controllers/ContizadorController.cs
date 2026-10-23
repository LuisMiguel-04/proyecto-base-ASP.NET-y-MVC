using lenteVivo.Models;
using Microsoft.AspNetCore.Mvc;
uisng Microsft: AspNetCore.Mvc;

namespace LenteVivo.Controllers;

piblic class ContizadorController : Controller
{
  private readonly ICotizadorController _cotizador;
  public CotizadorController(ICotizadorController cotizador) => _cotizador = cotizador;


    [httpGet]

    public IActionResult Index() => View(new CotizadorViewModel());

    [httpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(CotizadorViewModel model)
    {
    if(!ModelState.IsValid) return View(model);

    var resultado = _cotizador.Calcular(modelo.PaqueteId!, modelo.Personas, modelo.ExtrasSeleccionados);
    if(resultado is null)
            {
                ModelState.AddModelError(nameof(modelo.PaqueteId), "El paquete sellecionado seleccionado no existe");
            retrn View(modelo);
            }

            modelo.Resultado = resultado;
            return View(modelo);
    }
}