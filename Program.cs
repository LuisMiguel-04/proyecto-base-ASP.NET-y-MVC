using LenteVivo.Data;
using LenteVivo.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("LenteVivoDb")
    ?? throw new InvalidOperationException("Falta la cadena de conexión 'LenteVivoDb'.");

builder.Services.AddDbContext<LenteVivoContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<ICotizadorService, CotizadorService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
