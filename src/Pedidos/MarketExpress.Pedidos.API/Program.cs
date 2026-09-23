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
        builder.Configuration.GetConnectionString("Pedidos")));

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();