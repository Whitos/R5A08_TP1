﻿using BlazorApp.Models;
using System.Net.Http.Json;

namespace BlazorApp.Services
{
    public class WebService : IService<Produit>
    {
        private readonly HttpClient httpClient = new()
        {
            BaseAddress = new Uri("https://localhost:7234/api/")
        };

        public async Task AddAsync(Produit produit)
        {
            await httpClient.PostAsJsonAsync<Produit>("produits", produit);
        }

        public async Task DeleteAsync(int id)
        {
            await httpClient.DeleteAsync($"produits/{id}");
        }

        public async Task<List<Produit>?> GetAllAsync()
        {
            return await httpClient.GetFromJsonAsync<List<Produit>?>("produits");
        }

        public async Task<Produit?> GetByIdAsync(int id)
        {
            return await httpClient.GetFromJsonAsync<Produit?>($"produits/{id}");
        }

        public async Task<Produit?> GetByNameAsync(string name)
        {
            var response = await httpClient.PostAsJsonAsync("produits/search", name);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Produit>();
        }

        public async Task UpdateAsync(Produit updatedEntity)
        {
            await httpClient.PutAsJsonAsync<Produit>($"produits/{updatedEntity.IdProduit}", updatedEntity);
        }
    }
}
