using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LenteVivo.Models;

public class Paquete
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(50, ErrorMessage = "Máximo 50 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "La duración es obligatoria.")]
    [StringLength(50, ErrorMessage = "Máximo 50 caracteres.")]
    [Display(Name = "Duración")]
    public string Duracion { get; set; } = string.Empty;

    [Required(ErrorMessage = "Indica las fotos editadas.")]
    [StringLength(50, ErrorMessage = "Máximo 50 caracteres.")]
    [Display(Name = "Fotos editadas")]
    public string FotosEditadas { get; set; } = string.Empty;

    [Required(ErrorMessage = "Indica qué video incluye.")]
    [StringLength(50, ErrorMessage = "Máximo 50 caracteres.")]
    public string Video { get; set; } = string.Empty;

    [Range(1, 100000000, ErrorMessage = "Ingresa un precio válido.")]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Precio { get; set; }

    public ICollection<SolicitudSesion> Solicitudes { get; set; } = new List<SolicitudSesion>();
}
