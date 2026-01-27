using SharedDTOs.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace CustomerManager.Models
{
    public class CustomerService
    {
        private readonly HttpClient _httpClient;

        public CustomerService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7201/");
        }

        public async Task<CustomerDto[]> GetAsync()
        {
            var customers = await _httpClient.GetFromJsonAsync<CustomerDto[]>("api/customers");
            return customers ?? Array.Empty<CustomerDto>();
        }

        public async Task InsertAsync(CustomerDto customerDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/customers", customerDto);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(string customerCode)
        {
            var response = await _httpClient.DeleteAsync($"api/customers/{customerCode}");
            response.EnsureSuccessStatusCode();
        }
    }
}
