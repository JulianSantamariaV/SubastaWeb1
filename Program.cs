using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar el DbContext con la cadena de conexión.
builder.Services.AddDbContext<SubastaWebContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SubastaWebContext")
    ?? throw new InvalidOperationException("Connection string 'SubastaWebContext' not found.")));

// Agregar el servicio de autenticación y autorización
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Ingresar"; // Ruta donde se redirige si no está autenticado
        options.LogoutPath = "/Salir";   // Ruta para el logout (opcional)
    });

// Agregar servicios al contenedor.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// **Agrega el middleware de autenticación y autorización**
app.UseAuthentication();  // <-- Necesario para gestionar la autenticación
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
