using System.ComponentModel.DataAnnotations;

namespace LenteVivo.Models;

public enum TipoSesion
{
    Boda,
    Retrato,
    [Display(Name = "Evento corporativo")]
    Evento,
    Producto
}
public class SolicitudSesion
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
    [Display(Name = "Nombre completo")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "Ingresa un correo válido.")]
    [Display(Name = "Correo electrónico")]
    public string Correo { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Ingresa un teléfono válido.")]
    [Display(Name = "Teléfono")]
    public string? Telefono { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Fecha deseada para la sesión")]
    public DateTime? Fecha { get; set; }

    [Display(Name = "Tipo de sesión")]
    public TipoSesion TipoSesion { get; set; } = TipoSesion.Boda;

    public List<string> Adicionales { get; set; } = new();

    [Range(1, 500, ErrorMessage = "Indica entre 1 y 500 personas.")]
    [Display(Name = "Número aproximado de personas")]
    public int? Invitados { get; set; }

    [StringLength(1000, ErrorMessage = "El mensaje no puede superar 1000 caracteres.")]
    [Display(Name = "Cuéntanos sobre tu proyecto")]
    public string? Mensaje { get; set; }

    public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
}
