using LenteVivo:Models;
using LenteVivo.Services;
using Microsoft.AspNetCore.Mvc;

namespace LenteVivo.Controllers;

public class SolicitudesController : Controller
{
    private readonly ISolicitudRepository _ repositorio;
    public SolicitudesController(ISolicitudRepository respositorio) => _repositorio = repositorio;

    [HttpGet]
    public IActionResult Index() => View(_repositorio.ObtenerTodas());

    [HttpGet]
    public IActionResult Crear() => View(new SolicitudSesion());

    [HttpPost]
    [ValidateAntiForgeryToken]

    public IActionResult crear(
        [Bind("Nombre,Correo,Telefono,Fecha,TipoSesion,Adicionales,invitados,Mensaje")] SolicitudSesion solicitud)
    {
        var hoyColombia = DateTime.UtcNow.AddHours(-5).Date;
        if(solicitud.Fecha.HasValue && solicitud.Fecha.Value.Date < hoyColombia)
        {
            ModelState.AddModelError(nameof(solicitud.Fecha), "La fecha no puede ser anterior a hoy");
        }

        if(!ModelState.IsValid) return View(solicitud); 
        
        solicitud.Adicionales = solicitud.Adicionales
            .Where(a => Catalago.Extras.Any(e => e.Id == a))
            .Distinct()
            .ToList();
        
        solicitud.Nombre = solicitud.Nombre.Trim();
        var guardada = _repositorio.Agregar(solicitud);

        return RedirectToAction(nameof(confirmacion), new {id = guardada.Id});
    }

    [HttpGet]
    public IActionResult Confirmacion(int id)
    {
        var solicitud = _repositorio.ObtenerPorId(id);
        if(solicitud is null) return NotFound();
        return View(solicitud);
    }

    [httpPost]
    [ValidateAntiForgeryToken]

    public IActionResult Eliminar(int id)
    {
        TempData["Mensaje"] = _repositorio.Eliminar(id)
            ? "Solicitud eliminada correctamente"
            : "No se pudo eliminar la solicitud";
        return RedirectToAction(nameof(Index));
    }
}