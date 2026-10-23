namespace LenteVivo.Models;
public class HomeViewModel
{
    public IReadOnlyList<Servicio> Servicios { get; set; } = new List<Servicio>();
    public IReadOnlyList<PasoProceso> Proceso { get; set; } = new List<PasoProceso>();
    public IReadOnlyList<ItemGaleria> Galeria { get; set; } = new List<ItemGaleria>();
    public IReadOnlyList<Paquete> Paquetes { get; set; } = new List<Paquete>();
}
