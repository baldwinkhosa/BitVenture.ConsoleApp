using BitVenture.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BitVenture.Control
{
    public class ApiDataProviderService
    {
        protected ILogger _Logger { get; }

        public ApiDataProviderService()
        {
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                    .AddConsole();
            });

            _Logger = loggerFactory.CreateLogger<ApiDataProviderService>();
        }

        public virtual async Task<ApiResponse> GetServiceData(string baseURL, string resource)
        {
            ApiResponse result = new ApiResponse();
            string url;
            try
            {
                _Logger.LogInformation($"{nameof(ApiResponse)} getting data via API");
                url = baseURL + resource;

                using HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(baseURL);
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<ApiResponse>();
                }
            }
            catch (Exception exception)
            {
                _Logger.LogError(exception.ToString());
                throw;
            }
            return result;
        }

    }
}
