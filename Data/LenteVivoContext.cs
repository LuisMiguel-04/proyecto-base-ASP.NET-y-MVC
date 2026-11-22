using LenteVivo.Models;
using Microsoft.EntityFrameworkCore;

namespace LenteVivo.Data;

public class LenteVivoContext : DbContext
{
    public LenteVivoContext(DbContextOptions<LenteVivoContext> options) : base(options)
    {
    }

    public DbSet<Paquete> Paquetes => Set<Paquete>();
    public DbSet<SolicitudSesion> Solicitudes => Set<SolicitudSesion>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Paquete>(entity =>
        {
            entity.ToTable("Paquetes");
            entity.HasIndex(p => p.Nombre).IsUnique();
            entity.HasData(
                new Paquete { Id = 1, Nombre = "Básico",       Duracion = "2 horas",      FotosEditadas = "30",                Video = "No incluye",        Precio = 450000m },
                new Paquete { Id = 2, Nombre = "Estándar",     Duracion = "4 horas",      FotosEditadas = "60",                Video = "No incluye",        Precio = 750000m },
                new Paquete { Id = 3, Nombre = "Premium",      Duracion = "Día completo", FotosEditadas = "120",               Video = "Resumen de 3 min",  Precio = 1400000m },
                new Paquete { Id = 4, Nombre = "Premium Plus", Duracion = "Día completo", FotosEditadas = "Todas las tomadas", Video = "Resumen + tráiler", Precio = 1900000m });
        });

        modelBuilder.Entity<SolicitudSesion>(entity =>
        {
            entity.ToTable("Solicitudes");

            
            entity.Property(s => s.TipoSesion).HasConversion<string>().HasMaxLength(20);

            
            entity.HasOne(s => s.Paquete)
                  .WithMany(p => p.Solicitudes)
                  .HasForeignKey(s => s.PaqueteId)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
