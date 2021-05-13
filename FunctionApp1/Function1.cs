using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace sl4vik
{
    public static class Function1
    {
        [FunctionName("temperature")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            int temperature = Convert.ToInt32(req.Query["temperature"]);

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            int result = Convert.ToInt32(GetTemperature(temperature));

            if (String.IsNullOrEmpty(result.ToString()))
            {
                return new BadRequestObjectResult("No temperature provided");
            }
            else
            {
                return new OkObjectResult($"Heater temperature: {result} °C");
            }
        }
        private static string GetTemperature(int temperature)
        {
            if (temperature <= -15)
            {
                return "95";
            }
            else if (temperature <= -10)
            {
                return "83";
            }
            else if (temperature <= -5)
            {
                return "70";
            }
            else if (temperature <= 0)
            {
                return "57";
            }
            else if (temperature <= 5)
            {
                return "44";
            }
            else if (temperature <= 10)
            {
                return "30";
            }
            else
            {
                return Convert.ToString(temperature);
            }
        }
    }
}