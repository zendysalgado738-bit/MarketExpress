using System.Net;
using System.Net.Http.Json;
using MarketExpress.Pedidos.Aplicacion.DTOs;
using MarketExpress.Pedidos.Aplicacion.Interfaces;

namespace MarketExpress.Pedidos.API.Clientes;

public class ProductoCatalogoClient : IProductoCatalogoClient
{
    private readonly HttpClient _httpClient;

    public ProductoCatalogoClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ProductoCatalogoDto?> ObtenerPorIdAsync(Guid id)
    {
        var respuesta = await _httpClient.GetAsync($"api/productos/{id}");

        if (respuesta.StatusCode == HttpStatusCode.NotFound)
            return null;

        respuesta.EnsureSuccessStatusCode();

        return await respuesta.Content
            .ReadFromJsonAsync<ProductoCatalogoDto>();
    }

    public async Task<ProductoCatalogoDto?> ReservarStockAsync(
        Guid id,
        int cantidad)
    {
        var respuesta = await _httpClient.PostAsJsonAsync(
            $"api/productos/{id}/reservar-stock",
            new { cantidad });

        if (respuesta.StatusCode == HttpStatusCode.NotFound)
            return null;

        respuesta.EnsureSuccessStatusCode();

        return await respuesta.Content
            .ReadFromJsonAsync<ProductoCatalogoDto>();
    }
}