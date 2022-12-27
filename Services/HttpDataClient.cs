using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Fincra.Interfaces;
using Microsoft.Extensions.Logging;
using static System.Net.Mime.MediaTypeNames;

namespace Fincra.Services
{
    public class HttpDataClient : IHttpDataClient
    {
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;
        private JsonSerializerOptions serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        public HttpDataClient(IHttpClientFactory httpClientFactory, ILogger<HttpDataClient> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _logger = logger;
        }

        public async Task<T> MakeRequest<T>(string url)
        {
            var httpResponseMessage = await _httpClient.GetAsync(url);
            return await PrepareResponse<T>(httpResponseMessage);
        }

        public async Task<T> MakeRequest<T>(object data, string url, Dictionary<string, string> headers)
        {
            T response = default;
            try
            {
                string body = System.Text.Json.JsonSerializer.Serialize(data, serializeOptions).Replace("\n", "");
                var bodyData = new StringContent(
                   body,
                   Encoding.UTF8,
                   Application.Json
                );

                foreach (var header in headers)
                {
                    _httpClient.DefaultRequestHeaders.Clear();
                    _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }

                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var httpResponseMessage = await _httpClient.PostAsync(url, bodyData);
                response = await PrepareResponse<T>(httpResponseMessage);
            }
            catch (HttpRequestException exception)
            {
                _logger.LogError("MakeRequest", exception);
            }
            return response;
        }

        public async Task<T> MakeRequest<T>(string url, Dictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var httpResponseMessage = await _httpClient.GetAsync(url);
            return await PrepareResponse<T>(httpResponseMessage);
        }

        private async Task<T> PrepareResponse<T>(HttpResponseMessage httpResponseMessage)
        {
            T response = default;
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
                response = System.Text.Json.JsonSerializer.Deserialize<T>(contentStream, serializeOptions);
            }
            return response;
        }
    }
}