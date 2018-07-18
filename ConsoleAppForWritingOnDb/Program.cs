using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FunctionAppForWritingOnDb.Models;
using ITSRunningDbRepository;
using ITSRunningDbRepository.Models;
using Microsoft.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleAppForWritingOnDb
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();

            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                configuration.GetConnectionString("StorageConnectionString"));

            // Create the queue client.
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a queue.
            CloudQueue queue = queueClient.GetQueueReference(
                configuration.GetConnectionString("QueueName"));

            TelemetryRepository repository = new TelemetryRepository(
                configuration.GetConnectionString("RunnersDbConnectionString"));

            while (true)
            {
                var message = await queue.GetMessageAsync();
                if (message == null)
                {
                    await Task.Delay(1000);
                    continue;
                }
                /*
                JObject jsonMessage = JObject.Parse(message.AsString);

                int idActivity = (int)jsonMessage["ActivityId"];
                int idRunner = (int)jsonMessage["RunnerId"];

                IList<JToken> results = jsonMessage["Telemetry"].Children().ToList();
                var listOfCoordinates = new List<TelemetryModel>();
                foreach (JToken result in results)
                {
                    TelemetryModel searchResult = result.ToObject<TelemetryModel>();
                    listOfCoordinates.Add(searchResult);
                }
                */

                var messageDeserialized = Newtonsoft.Json.JsonConvert.DeserializeObject<TelemetryModel>(message.AsString);

                var telemetry = new Telemetry()
                {
                    Latitude = messageDeserialized.Latitude,
                    Longitude = messageDeserialized.Longitude,
                    Instant = messageDeserialized.Instant,
                    IdActivity = messageDeserialized.ActivityId,
                    SelfiUri = null
                };
                repository.Insert(telemetry);

                Console.WriteLine(message.Id + " - is on databese");

                //delete item in queue when message is inserted on database
                await queue.DeleteMessageAsync(message);
            }
        }
    }
}
