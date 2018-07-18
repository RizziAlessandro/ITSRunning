using System;
using System.Collections;
using System.Collections.Generic;
using FunctionAppForWritingOnDb.Models;
using ITSRunningDbRepository;
using ITSRunningDbRepository.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace FunctionAppForWritingOnDb
{
    public static class Function1
    {

        [FunctionName("Function1")]
        public static void Run([QueueTrigger("coordinates-queue", Connection = "StorageConnectionString")]string queueItem, TraceWriter log)
        {
            var listOfCoordinates = JsonConvert.DeserializeObject<IEnumerable<TelemetryMessageModel>>(queueItem);

            string cs = Environment.GetEnvironmentVariable("RunnersDbConnectionString", EnvironmentVariableTarget.Process);
            TelemetryRepository repository = new TelemetryRepository(cs);

            foreach (TelemetryMessageModel item in listOfCoordinates)
            {
                var telemetry = new Telemetry()
                {
                    Latitude = item.Latitude,
                    Longitude = item.Longitude,
                    Instant = DateTimeOffset.Now,
                    IdRunner = 1,
                    IdActivity = 1,
                    SelfiUri = "fodengURI"
                };
                //ERROR: file or assembly not found
                repository.Insert(telemetry);
            }
        }
    }
}
