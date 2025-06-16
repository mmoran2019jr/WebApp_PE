using DataAccess.BsnLogic;
using DataAccess.BsnLogic.Interfaces;
using DataAccess.BsnLogic.Repositories;
using DataAccess.BsnLogic.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// Agregar DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";     // si el usuario no está logueado
        options.LogoutPath = "/Account/Logout";   // logout
        options.AccessDeniedPath = "/Account/Denied";
    });

//Agregar autorizacion
builder.Services.AddAuthorization();


//Inyeccion de dependencias
builder.Services.AddScoped<AuthenticationService>();

builder.Services.AddScoped<IFormulaRepository, FormulaRepository>();
builder.Services.AddScoped<IFormulaService, FormulaService>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IProductoService, ProductoService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DataAccess.BsnLogic.ApplicationDbContext>();

    // Ejecutar semilla
    DbInitializer.Seed(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
