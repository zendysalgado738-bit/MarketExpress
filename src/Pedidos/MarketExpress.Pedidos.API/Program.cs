using MarketExpress.Pedidos.API.Clientes;
using MarketExpress.Pedidos.Aplicacion.Interfaces;
using MarketExpress.Pedidos.Aplicacion.Servicios;
using MarketExpress.Pedidos.Infraestructura.Persistencia;
using MarketExpress.Pedidos.Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PedidosDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Pedidos"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null)));

builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IPedidoService, PedidoService>();

builder.Services.AddHttpClient<
    IProductoCatalogoClient,
    ProductoCatalogoClient>(cliente =>
    {
        var catalogoUrl = builder.Configuration["Servicios:CatalogoUrl"]
            ?? "http://localhost:5062/";

        cliente.BaseAddress = new Uri(catalogoUrl);
    });

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