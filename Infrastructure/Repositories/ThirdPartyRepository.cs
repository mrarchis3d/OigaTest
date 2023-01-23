using Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using System.Web;

namespace Business.Repositories
{
    public class ThirdPartyRepository : IThirdPartyRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ThirdPartyRepository> _logger;
        private string url;

        public ThirdPartyRepository(HttpClient httpClient, IConfiguration configuration, ILogger<ThirdPartyRepository> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _configuration = configuration;
            url = _configuration.GetSection("AzureFunctions").Value;
        }

        public async Task<T> GetAsync<T>(object? obj, string command) where T : class, new()
        {
            _logger.LogInformation($"Enter In ThirdpartyMethods Get to -> {command}");
            string query = string.Empty;
            if (obj != null)
                query = QueryBuilder(obj);
            var response = await _httpClient.GetAsync(url + command + query);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            _logger.LogInformation($"Content In ThirdpartyMethods Get to -> {command}");
            return JsonConvert.DeserializeObject<T>(content);
        }

        public async Task<T> Post<T>(object obj, string command)
        {
            _logger.LogInformation($"Enter In ThirdpartyMethods Post to -> {command}");
            var content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url + command, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            _logger.LogInformation($"Content In ThirdpartyMethods Post to -> {command}");
            return JsonConvert.DeserializeObject<T>(responseContent);
        }

        private string QueryBuilder(object obj)
        {
            var queryString = HttpUtility.ParseQueryString("");
            var properties = obj.GetType().GetProperties();
            foreach (var prop in properties)
            {
                queryString[prop.Name] = prop.GetValue(obj)?.ToString();
            }

            return "?" + queryString.ToString();
        }
    }
}
