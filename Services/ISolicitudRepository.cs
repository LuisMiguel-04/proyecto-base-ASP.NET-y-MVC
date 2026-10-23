using LenteVivo.Models;

namespace LenteVivo.Services;

public interface ISolicitudRepository
{
    IReadOnlyList<SolicitudSesion> ObtenerTodas();
    SolicitudSesion? ObtenerPorId(int id);
    SolicitudSesion Agregar(SolicitudSesion solicitud);
    bool Eliminar(int id);
}
