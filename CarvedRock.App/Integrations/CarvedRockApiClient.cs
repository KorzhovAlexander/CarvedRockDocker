using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CarvedRock.App.Models;
using Microsoft.Extensions.Configuration;

namespace CarvedRock.App.Integrations
{
    public class CarvedRockApiClient : ICarvedRockApiClient
    {
        private readonly HttpClient _client;

        public CarvedRockApiClient(HttpClient client, IConfiguration config)
        {
            _client = client;
            _client.BaseAddress = new Uri(config.GetValue<string>("CarvedRockApiUrl"));
        }

        public async Task<List<Product>> GetProducts(string category = null)
        {
            var requestUri = "products";
            if (!string.IsNullOrWhiteSpace(category))
            {
                requestUri += $"?category={category}";
            }

            return await _client.GetFromJsonAsync<List<Product>>(requestUri);
        }

        public async Task<Guid> PlaceQuickOrder(QuickOrder order)
        {
            var response = await _client.PostAsJsonAsync("quickorder", order);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Guid>();
            }

            var ex = new Exception("Failed placing QuickOrder.");
            ex.Data.Add("Product", order.ProductId);
            ex.Data.Add("Quantity", order.Quantity);
            ex.Data.Add("URI", response.RequestMessage?.RequestUri);
            ex.Data.Add("ResponseCode", (int)response.StatusCode);
            throw ex;
        }
    }
}
