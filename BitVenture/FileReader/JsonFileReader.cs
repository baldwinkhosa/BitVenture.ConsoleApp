using BitVenture.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;

namespace BitVenture.FileReader
{
    public class JsonFileReader
    {

        public static RootObject ReadJsonFile (string filePath)
        {
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                    .AddConsole();
            });

            ILogger logger = loggerFactory.CreateLogger<JsonFileReader>();

            try
            {
                logger.LogInformation($"{nameof(JsonFileReader)} Reading JSON file");

                using (StreamReader r = new StreamReader(filePath))
                {
                    string json = r.ReadToEnd();
                    return JsonConvert.DeserializeObject<RootObject>(json);
                }
            }
            catch(Exception ex)
            {
                logger.LogError(ex.ToString());
                throw;
            }
            
        }    
    }
}
