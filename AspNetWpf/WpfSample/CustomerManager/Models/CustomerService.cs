using SharedDTOs.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
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

        public async Task<CustomerDto[]> GetCustomersAsync()
        {
            // API を叩く
            var customers = await _httpClient.GetFromJsonAsync<CustomerDto[]>("api/customers");
            return customers ?? Array.Empty<CustomerDto>();
        }
    }
}
