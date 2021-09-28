using BitVenture.Control;
using BitVenture.FileReader;
using BitVenture.Validate_Response;
using Microsoft.Extensions.Logging;
using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace BitVenture
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                    .AddConsole();
            });

            ILogger logger = loggerFactory.CreateLogger<Program>();

            string path;

            try
            {
                logger.LogInformation("----BitVenture Console Application - Start");

                path = ConfigurationManager.AppSettings.Get("path");
                var jsonFileData = JsonFileReader.ReadJsonFile(path);

                ApiDataProviderService apiDataProviderService = new ApiDataProviderService();
                ValidateResponse validateResponse = new ValidateResponse();

                foreach (var data in jsonFileData.services)
                {
                    foreach(var endpoint in data.Endpoints)
                    {
                        var jsonResponse = await apiDataProviderService.GetServiceData(data.BaseURL, endpoint.Resource);
                        var valid = validateResponse.ValidateElement(endpoint.response, jsonResponse);

                        if(valid.Any())
                        {
                            Console.WriteLine($"API response is valid for {data.BaseURL}{endpoint.Resource} and the element value is {valid.Select( x => x).ToList()}");
                        }
                        else
                        {
                            Console.WriteLine($"API response is not valid for {data.BaseURL}{endpoint.Resource}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
            }
            
        }


    }
}
