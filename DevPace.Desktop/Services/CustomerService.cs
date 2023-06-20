using DevPace.Desktop.Models.CustomerModels;
using DevPace.Desktop.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DevPace.Desktop.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;

        public CustomerService(HttpClient httpClient, string apiUrl)
        {
            _httpClient = httpClient;
            _apiUrl = apiUrl;
        }

        public async Task<CustomerModel> GetCustomerByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}/Customer/{id}");
            response.EnsureSuccessStatusCode();
            var customer = await response.Content.ReadAsAsync<CustomerModel>();
            return customer;
        }

        public async Task<IEnumerable<CustomerModel>> GetCustomersAsync(int pageNumber, int rowCount)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}/Customer?pageNumber={pageNumber}&rowCount={rowCount}");
            response.EnsureSuccessStatusCode();
            var customers = await response.Content.ReadAsAsync<IEnumerable<CustomerModel>>();
            return customers;
        }

        public async Task<IEnumerable<CustomerModel>> GetCustomersAsync(int pageNumber, int rowCount, string searchItem, string orderBy, bool orderByDescending)
        {
            var url = $"{_apiUrl}/Customer?pageNumber={pageNumber}&rowCount={rowCount}&searchItem={searchItem}&orderBy={orderBy}&orderByDescending={orderByDescending}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var customers = await response.Content.ReadAsAsync<IEnumerable<CustomerModel>>();
            return customers;
        }

        public async Task<CustomerModel> CreateCustomerAsync(CustomerCreateModel customerCreate)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_apiUrl}/Customer", customerCreate);
            response.EnsureSuccessStatusCode();
            var createdCustomer = await response.Content.ReadAsAsync<CustomerModel>();
            return createdCustomer;
        }

        public async Task UpdateCustomerAsync(CustomerUpdateModel customerUpdate)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_apiUrl}/Customer", customerUpdate);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCustomerAsync(CustomerDeleteModel customerDelete)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{_apiUrl}/Customer")
            {
                Content = new StringContent(JsonConvert.SerializeObject(customerDelete), Encoding.UTF8, "application/json")
            };
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
    }
}
