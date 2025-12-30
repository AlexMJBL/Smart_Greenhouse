using Greenhouse_Data_MVC.Interfaces;

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
            _httpClient.BaseAddress =
                new Uri(config["ApiSettings:BaseUrl"] + "/" + endpoint);
        }

        public async Task<List<T>> GetAllAsync()
            => await _httpClient.GetFromJsonAsync<List<T>>("");

        public async Task<T?> GetByIdAsync(int id)
            => await _httpClient.GetFromJsonAsync<T>($"{id}");
        public async Task DeleteAsync(int id)
            => await _httpClient.DeleteAsync($"{id}");
    }

}
