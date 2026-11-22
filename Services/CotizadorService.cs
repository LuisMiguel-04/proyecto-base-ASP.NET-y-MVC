using LenteVivo.Data;
using LenteVivo.Models;

namespace LenteVivo.Services;

public interface ICotizadorService
{
    Task<CotizacionResultado?> CalcularAsync(int paqueteId, int personas, IEnumerable<string> extrasIds);
}

public class CotizadorService : ICotizadorService
{
    public const decimal RecargoPorPersona = 5000m;
    public const int LimitePersonasSinRecargo = 20;

    private readonly LenteVivoContext _context;

    public CotizadorService(LenteVivoContext context)
    {
        _context = context;
    }

    public async Task<CotizacionResultado?> CalcularAsync(int paqueteId, int personas, IEnumerable<string> extrasIds)
    {
        var paquete = await _context.Paquetes.FindAsync(paqueteId);
        if (paquete is null) return null;

        var extras = Catalogo.Extras.Where(e => extrasIds.Contains(e.Id)).ToList();

        var personasConRecargo = Math.Max(0, personas - LimitePersonasSinRecargo);
        var recargo = personasConRecargo * RecargoPorPersona;
        var total = paquete.Precio + extras.Sum(e => e.Precio) + recargo;

        return new CotizacionResultado(paquete, extras, personas, personasConRecargo, recargo, total);
    }
}
