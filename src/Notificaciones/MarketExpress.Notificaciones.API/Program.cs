using MarketExpress.Notificaciones.Aplicacion.Interfaces;
using MarketExpress.Notificaciones.Aplicacion.Servicios;
using MarketExpress.Notificaciones.Infraestructura.Persistencia;
using MarketExpress.Notificaciones.Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NotificacionesDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Notificaciones"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null)));

builder.Services.AddScoped<
    INotificacionRepository,
    NotificacionRepository>();

builder.Services.AddScoped<
    INotificacionService,
    NotificacionService>();

var app = builder.Build();

var swaggerHabilitado =
    app.Environment.IsDevelopment() ||
    app.Configuration.GetValue<bool>("Swagger:Enabled");

if (swaggerHabilitado)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();