using LenteVivo.Models;

namespace LenteVivo.Services;

public class SolicitudRepository : ISolicitudRepository
{
    private readonly List<SolicitudSesion> _solicitudes = new();
    private readonly object _candado = new();
    private int _contador;

    public IReadOnlyList<SolicitudSesion> ObtenerTodas()
    {
        lock (_candado) return _solicitudes.OrderBy(s => s.Id).ToList();
    }

    public SolicitudSesion? ObtenerPorId(int id)
    {
        lock (_candado) return _solicitudes.FirstOrDefault(s => s.Id == id);
    }

    public SolicitudSesion Agregar(SolicitudSesion solicitud)
    {
        lock (_candado)
        {
            solicitud.Id = ++_contador;
            solicitud.FechaRegistro = DateTime.UtcNow;
            _solicitudes.Add(solicitud);
            return solicitud;
        }
    }

    public bool Eliminar(int id)
    {
        lock (_candado) return _solicitudes.RemoveAll(s => s.Id == id) > 0;
    }
}
