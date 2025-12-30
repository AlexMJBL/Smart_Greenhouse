using Greenhouse_Data_MVC.Interfaces;

namespace Greenhouse_Data_MVC.Services
{
    public class ServiceProxy<T> : IServiceProxy<T> where T : class
    {
        protected readonly IHttpClientFactory _httpClientFactory;
        protected readonly string _baseUrl;

        protected ServiceProxy(IHttpClientFactory httpClientFactory, string baseUrl)
        {
            _httpClientFactory = httpClientFactory;
            _baseUrl = baseUrl;
        }

        protected HttpClient CreateClient()
        {
            return _httpClientFactory.CreateClient("GreenhouseAPI");
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var client = CreateClient();
            return await client.GetFromJsonAsync<IEnumerable<T>>(_baseUrl) ?? Enumerable.Empty<T>();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            var client = CreateClient();
            return await client.GetFromJsonAsync<T>($"{_baseUrl}/{id}");
        }

        public async Task DeleteAsync(int id)
        {
            var client = CreateClient();
            var response = await client.DeleteAsync($"{_baseUrl}/{id}");

            response.EnsureSuccessStatusCode();
        }
    }
}
 