using System.ComponentModel.DataAnnotations;

namespace LenteVivo.Models;

public class CotizadorViewModel
{
    [Required(ErrorMessage = "Selecciona un paquete.")]
    [Display(Name = "Paquete")]
    public string? PaqueteId { get; set; }

    [Range(1, 500, ErrorMessage = "Indica entre 1 y 500 personas.")]
    [Display(Name = "Número de personas")]
    public int Personas { get; set; } = 20;

    public List<string> ExtrasSeleccionados { get; set; } = new();

    public CotizacionResultado? Resultado { get; set; }
}

public record CotizacionResultado(
    Paquete Paquete,
    IReadOnlyList<ExtraServicio> Extras,
    int Personas,
    int PersonasConRecargo,
    decimal Recargo,
    decimal Total);
