namespace LenteVivo.Models;

public record Paquete(string Id, string Nombre, string Duracion, string FotosEditadas, string Video, decimal Precio);

public record ExtraServicio(string Id, string Nombre, decimal Precio);

public record Servicio(string Icono, string Titulo, string Descripcion);

public record PasoProceso(string Titulo, string Descripcion);

public record ItemGaleria(string Categoria, string Imagen, string TextoAlternativo, string Titulo);
