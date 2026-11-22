using LenteVivo.Data;
using LenteVivo.Models;
using LenteVivo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LenteVivo.Controllers;

public class SolicitudesController : Controller
{
    private const string CamposPermitidos =
        "Id,Nombre,Correo,Telefono,Fecha,TipoSesion,PaqueteId,Adicionales,Invitados,Mensaje";

    private readonly LenteVivoContext _context;

    public SolicitudesController(LenteVivoContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var solicitudes = await _context.Solicitudes
            .Include(s => s.Paquete)
            .AsNoTracking()
            .OrderBy(s => s.Id)
            .ToListAsync();

        return View(solicitudes);
    }


    [HttpGet]
    public async Task<IActionResult> Crear(int? paqueteId)
    {
        await CargarPaquetesAsync(paqueteId);
        return View(new SolicitudSesion { PaqueteId = paqueteId });
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(
        [Bind("Nombre,Correo,Telefono,Fecha,TipoSesion,PaqueteId,Adicionales,Invitados,Mensaje")]
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

        await ValidarPaqueteAsync(solicitud);

        if (!ModelState.IsValid)
        {
            await CargarPaquetesAsync(solicitud.PaqueteId);
            return View(solicitud);
        }

        solicitud.Adicionales = LimpiarAdicionales(solicitud.Adicionales);
        solicitud.Nombre = solicitud.Nombre.Trim();
        solicitud.FechaRegistro = DateTime.UtcNow;

        _context.Solicitudes.Add(solicitud);
        await _context.SaveChangesAsync();   // aquí se ejecuta el INSERT

        return RedirectToAction(nameof(Confirmacion), new { id = solicitud.Id });
    }


    [HttpGet]
    public async Task<IActionResult> Confirmacion(int id)
    {
        var solicitud = await _context.Solicitudes
            .Include(s => s.Paquete)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);

        if (solicitud is null)
        {
            return NotFound();
        }

        return View(solicitud);
    }

    [HttpGet]
    public async Task<IActionResult> Editar(int id)
    {
        var solicitud = await _context.Solicitudes.FindAsync(id);

        if (solicitud is null)
        {
            return NotFound();
        }

        await CargarPaquetesAsync(solicitud.PaqueteId);
        return View(solicitud);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(
        int id,
        [Bind(CamposPermitidos)] SolicitudSesion datos)
    {
        if (id != datos.Id)
        {
            return NotFound();
        }

        var solicitud = await _context.Solicitudes.FindAsync(id);

        if (solicitud is null)
        {
            return NotFound();
        }

        await ValidarPaqueteAsync(datos);

        if (!ModelState.IsValid)
        {
            await CargarPaquetesAsync(datos.PaqueteId);
            return View(datos);
        }

        solicitud.Nombre = datos.Nombre.Trim();
        solicitud.Correo = datos.Correo;
        solicitud.Telefono = datos.Telefono;
        solicitud.Fecha = datos.Fecha;
        solicitud.TipoSesion = datos.TipoSesion;
        solicitud.PaqueteId = datos.PaqueteId;
        solicitud.Adicionales = LimpiarAdicionales(datos.Adicionales);
        solicitud.Invitados = datos.Invitados;
        solicitud.Mensaje = datos.Mensaje;

        await _context.SaveChangesAsync();  

        TempData["Mensaje"] = "Solicitud actualizada correctamente.";
        return RedirectToAction(nameof(Index));
    }

   
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Eliminar(int id)
    {
        var solicitud = await _context.Solicitudes.FindAsync(id);

        if (solicitud is null)
        {
            TempData["Mensaje"] = "No se pudo eliminar la solicitud.";
        }
        else
        {
            _context.Solicitudes.Remove(solicitud);
            await _context.SaveChangesAsync();
            TempData["Mensaje"] = "Solicitud eliminada correctamente.";
        }

        return RedirectToAction(nameof(Index));
    }

    
    private async Task CargarPaquetesAsync(int? seleccionado)
    {
        var paquetes = await _context.Paquetes
            .AsNoTracking()
            .OrderBy(p => p.Precio)
            .Select(p => new { p.Id, Texto = p.Nombre + " (" + p.Duracion + ")" })
            .ToListAsync();

        ViewBag.Paquetes = new SelectList(paquetes, "Id", "Texto", seleccionado);
    }

    private async Task ValidarPaqueteAsync(SolicitudSesion solicitud)
    {
        if (solicitud.PaqueteId.HasValue &&
            !await _context.Paquetes.AnyAsync(p => p.Id == solicitud.PaqueteId.Value))
        {
            ModelState.AddModelError(
                nameof(solicitud.PaqueteId),
                "El paquete seleccionado no existe.");
        }
    }

    private static List<string> LimpiarAdicionales(List<string> adicionales) =>
        adicionales
            .Where(id => Catalogo.Extras.Any(extra => extra.Id == id))
            .Distinct()
            .ToList();
}
