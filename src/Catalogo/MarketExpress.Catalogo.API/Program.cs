using MarketExpress.Catalogo.Aplicacion.Interfaces;
using MarketExpress.Catalogo.Aplicacion.Servicios;
using MarketExpress.Catalogo.Infraestructura.Persistencia;
using MarketExpress.Catalogo.Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CatalogoDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Catalogo"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null)));

builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IProductoService, ProductoService>();

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