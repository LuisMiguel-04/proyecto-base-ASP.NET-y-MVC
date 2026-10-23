using System.Globalization;

namespace LenteVivo.Helpers;

public static class Formato
{
    private static readonly CultureInfo Colombia = CultureInfo.GetCultureInfo("es-CO");

    public static string Moneda(this decimal valor) => valor.ToString("C0", Colombia);
}
