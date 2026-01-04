using System.Net;
using System.Net.Http.Json;

namespace Greenhouse_Data_MVC.Services
{
    public class ServiceProxy<T>
      where T : class
    {
        protected readonly HttpClient _httpClient;

        public ServiceProxy(
            IHttpClientFactory factory,
            IConfiguration config,
            string endpoint)
        {
            _httpClient = factory.CreateClient();
            _httpClient.BaseAddress = new Uri(
                $"{config["ApiSettings:BaseUrl"]}/api/{endpoint}/"
            );
        }

        public async Task<List<T>> GetAllAsync()
            => await _httpClient.GetFromJsonAsync<List<T>>("");

        public async Task<T?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{id}");
            response.EnsureSuccessStatusCode();
        }
    }

}
