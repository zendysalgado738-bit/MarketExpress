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
        builder.Configuration.GetConnectionString("Notificaciones")));

builder.Services.AddScoped<
    INotificacionRepository,
    NotificacionRepository>();

builder.Services.AddScoped<
    INotificacionService,
    NotificacionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();