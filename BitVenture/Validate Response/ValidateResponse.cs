using BitVenture.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BitVenture.Validate_Response
{
    public class ValidateResponse
    {
        protected ILogger logger { get; }
        
        public ValidateResponse()
        {
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                    .AddConsole();
            });

            logger = loggerFactory.CreateLogger<ValidateResponse>();
        }
        public List<string> ValidateElement(List<Response> responses, ApiResponse apiResponse)
        {
            logger.LogInformation($"{nameof(ValidateElement)} Validating JSON Response");
            List<string> result = new List<string>();

            try
            {
                foreach (var response in responses)
                {
                    result.Add(GetPropValue(response.Element, responses).ToString());
                }
            }
            catch(Exception ex)
            {
                logger.LogError(ex.ToString());
                throw;
            }
            return result;
        }

        private static Object GetPropValue(String name, object src)
        {
            var parts = name.Split('.').ToList();
            var currentPart = parts[0];

            PropertyInfo info = src.GetType().GetProperty(currentPart);

            if (info == null)
            { 
                return null; 
            }

            if (name.IndexOf(".") > -1)
            {
                parts.Remove(currentPart);
                return GetPropValue(String.Join(".", parts), info.GetValue(src, null));
            }
            else
            {
                return info.GetValue(src, null).ToString();
            }
        }
    }
}
