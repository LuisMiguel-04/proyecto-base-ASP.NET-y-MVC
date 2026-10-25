using LenteVivo.Models;
using LenteVivo.Services;
using Microsoft.AspNetCore.Mvc;

namespace LenteVivo.Controllers;

public class SolicitudesController : Controller
{
    private readonly ISolicitudRepository _repositorio;

    public SolicitudesController(ISolicitudRepository repositorio)
    {
        _repositorio = repositorio;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(_repositorio.ObtenerTodas());
    }

    [HttpGet]
    public IActionResult Crear()
    {
        return View(new SolicitudSesion());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Crear(
        [Bind("Nombre,Correo,Telefono,Fecha,TipoSesion,Adicionales,Invitados,Mensaje")]
        SolicitudSesion solicitud)
    {
        var hoyColombia = DateTime.UtcNow.AddHours(-5).Date;

        if (solicitud.Fecha.HasValue &&
            solicitud.Fecha.Value.Date < hoyColombia)
        {
            ModelState.AddModelError(
                nameof(solicitud.Fecha),
                "La fecha no puede ser anterior a hoy.");
        }

        if (!ModelState.IsValid)
        {
            return View(solicitud);
        }

        solicitud.Adicionales = solicitud.Adicionales
            .Where(id => Catalogo.Extras.Any(extra => extra.Id == id))
            .Distinct()
            .ToList();

        solicitud.Nombre = solicitud.Nombre.Trim();

        var guardada = _repositorio.Agregar(solicitud);

        return RedirectToAction(
            nameof(Confirmacion),
            new { id = guardada.Id });
    }

    [HttpGet]
    public IActionResult Confirmacion(int id)
    {
        var solicitud = _repositorio.ObtenerPorId(id);

        if (solicitud is null)
        {
            return NotFound();
        }

        return View(solicitud);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Eliminar(int id)
    {
        TempData["Mensaje"] = _repositorio.Eliminar(id)
            ? "Solicitud eliminada correctamente."
            : "No se pudo eliminar la solicitud.";

        return RedirectToAction(nameof(Index));
    }
}