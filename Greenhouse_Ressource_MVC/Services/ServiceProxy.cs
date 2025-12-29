using Greenhouse_Ressource_MVC.Interfaces;

namespace Greenhouse_Ressource_MVC.Services
{
    public class ServiceProxy<T,U> : IServiceProxy<T, U> where T : class where U : class
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

        public async Task<T> CreateAsync(U entity)
        {
            var client = CreateClient();
            var response = await client.PostAsJsonAsync(_baseUrl, entity);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>() ?? throw new Exception("Error while creating the entity");
        }

        public async Task DeleteAsync(int id)
        {
            var client = CreateClient();
            var response = await client.DeleteAsync($"{_baseUrl}/{id}");

            response.EnsureSuccessStatusCode();
        }

        public async Task<T> UpdateAsync(int id, U entity)
        {
            var client = CreateClient();
            var response = await client.PutAsJsonAsync($"{_baseUrl}/{id}", entity);

            response.EnsureSuccessStatusCode();


            if (response.Content.Headers.ContentLength == 0)
                throw new Exception("API returned no content after update");

            return await response.Content.ReadFromJsonAsync<T>() ?? throw new Exception("Error while updating the entity");
        }
    }
}
 