using LenteVivo.Data;
using LenteVivo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LenteVivo.Controllers;

public class PaquetesController : Controller
{
    private const string CamposPermitidos = "Id,Nombre,Duracion,FotosEditadas,Video,Precio";

    private readonly LenteVivoContext _context;

    public PaquetesController(LenteVivoContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var paquetes = await _context.Paquetes
            .Include(p => p.Solicitudes)
            .AsNoTracking()
            .OrderBy(p => p.Precio)
            .ToListAsync();

        return View(paquetes);
    }

    [HttpGet]
    public IActionResult Crear() => View(new Paquete());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear([Bind(CamposPermitidos)] Paquete paquete)
    {
        await ValidarNombreUnicoAsync(paquete);

        if (!ModelState.IsValid)
        {
            return View(paquete);
        }

        _context.Paquetes.Add(paquete);
        await _context.SaveChangesAsync();

        TempData["Mensaje"] = "Paquete creado correctamente.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Editar(int id)
    {
        var paquete = await _context.Paquetes.FindAsync(id);

        if (paquete is null)
        {
            return NotFound();
        }

        return View(paquete);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(int id, [Bind(CamposPermitidos)] Paquete datos)
    {
        if (id != datos.Id)
        {
            return NotFound();
        }

        var paquete = await _context.Paquetes.FindAsync(id);

        if (paquete is null)
        {
            return NotFound();
        }

        await ValidarNombreUnicoAsync(datos);

        if (!ModelState.IsValid)
        {
            return View(datos);
        }

        paquete.Nombre = datos.Nombre.Trim();
        paquete.Duracion = datos.Duracion;
        paquete.FotosEditadas = datos.FotosEditadas;
        paquete.Video = datos.Video;
        paquete.Precio = datos.Precio;

        await _context.SaveChangesAsync();

        TempData["Mensaje"] = "Paquete actualizado correctamente.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Eliminar(int id)
    {
        var paquete = await _context.Paquetes.FindAsync(id);

        if (paquete is null)
        {
            TempData["Mensaje"] = "No se encontró el paquete.";
        }
        else if (await _context.Solicitudes.AnyAsync(s => s.PaqueteId == id))
        {
            TempData["Mensaje"] = "No se puede eliminar: el paquete tiene solicitudes asociadas.";
        }
        else
        {
            _context.Paquetes.Remove(paquete);
            await _context.SaveChangesAsync();
            TempData["Mensaje"] = "Paquete eliminado correctamente.";
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task ValidarNombreUnicoAsync(Paquete paquete)
    {
        var nombre = paquete.Nombre?.Trim();

        if (!string.IsNullOrEmpty(nombre) &&
            await _context.Paquetes.AnyAsync(p => p.Nombre == nombre && p.Id != paquete.Id))
        {
            ModelState.AddModelError(nameof(paquete.Nombre), "Ya existe un paquete con ese nombre.");
        }
    }
}
