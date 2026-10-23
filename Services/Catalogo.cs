using LenteVivo.Models;

namespace LenteVivo.Services;

public static class Catalogo
{
    public static readonly IReadOnlyList<Paquete> Paquetes = new List<Paquete>
    {
        new("basico",       "Básico",       "2 horas",       "30",                "No incluye",        450000m),
        new("estandar",     "Estándar",     "4 horas",       "60",                "No incluye",        750000m),
        new("premium",      "Premium",      "Día completo",  "120",               "Resumen de 3 min", 1400000m),
        new("premium-plus", "Premium Plus", "Día completo",  "Todas las tomadas", "Resumen + tráiler", 1900000m)
    };

    public static readonly IReadOnlyList<ExtraServicio> Extras = new List<ExtraServicio>
    {
        new("video", "Video resumen",   180000m),
        new("album", "Álbum impreso",   150000m),
        new("dron",  "Tomas con dron",  200000m)
    };

    public static readonly IReadOnlyList<Servicio> Servicios = new List<Servicio>
    {
        new("bi-heart-fill",          "Bodas",                     "Cobertura completa del evento, desde la previa hasta la fiesta."),
        new("bi-person-bounding-box", "Retratos",                  "Sesiones individuales, familiares y de pareja en estudio o exterior."),
        new("bi-building",            "Eventos corporativos",      "Cobertura de conferencias, lanzamientos y celebraciones empresariales."),
        new("bi-box-seam",            "Fotografía de producto",    "Imágenes profesionales para catálogos y tiendas en línea."),
        new("bi-stars",               "Sesiones de moda",          "Books fotográficos para modelos y marcas."),
        new("bi-mortarboard-fill",    "Fotografía de graduación",  "Sesiones individuales y grupales.")
    };

    public static readonly IReadOnlyList<PasoProceso> Proceso = new List<PasoProceso>
    {
        new("1. Consulta inicial",            "Definimos juntos la idea o concepto de la sesión."),
        new("2. Cotización y agendamiento",   "Te enviamos la cotización y agendamos la fecha de la sesión."),
        new("3. Sesión fotográfica",          "Realizamos la sesión con nuestro equipo especializado."),
        new("4. Edición y retoque",           "Editamos y retocamos profesionalmente cada imagen."),
        new("5. Entrega",                     "Entrega digital e impresa según el paquete contratado.")
    };

    public static readonly IReadOnlyList<ItemGaleria> Galeria = new List<ItemGaleria>
    {
        new("boda",     "Imagenes/boda.jpg",     "Sesión de fotografía de boda al aire libre", "Bodas"),
        new("retrato",  "Imagenes/retrato.jpg",  "Retrato profesional en estudio",             "Retratos"),
        new("evento",   "Imagenes/Evento.jpg",   "Cobertura de evento corporativo",            "Eventos"),
        new("producto", "Imagenes/producto.jpg", "Fotografía de producto para catálogo",       "Producto")
    };

    public static IEnumerable<string> NombresExtras(IEnumerable<string> ids) =>
        Extras.Where(e => ids.Contains(e.Id)).Select(e => e.Nombre);
}
