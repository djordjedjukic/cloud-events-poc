using System.Net;
using CloudNative.CloudEvents;
using CloudNative.CloudEvents.Http;
using CloudNative.CloudEvents.Kafka;
using Newtonsoft.Json;
using CloudNative.CloudEvents.NewtonsoftJson;
using Confluent.Kafka;
public record GameResult(string GameId, string PlayerId, int Score);

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Produce();
        }

        static void Produce()
        {
            var config = new ProducerConfig()
            {
                BootstrapServers = "localhost:9092"
            };

            var producer = new ProducerBuilder<Null, string>(config).Build();

            for (var i = 0; i < 5; ++i)
            {
                var value = $"Hello World: {i}";
    
                producer.Produce("demo", new Message<Null, string>()
                {
                    Value = value
                });
            }
        }

        private static void HttpExample()
        {
            var data = new GameResult("Game1", "Player1", 5);

            var cloudEvent = new CloudEvent
            {
                Id = Guid.NewGuid().ToString(),
                Type = "ravendb.etl.put",
                Source = new Uri("https://ravendb.net/"),
                DataContentType = "application/json",
                Data = JsonConvert.SerializeObject(data),
            };

            var content = cloudEvent.ToHttpContent(ContentMode.Binary, new JsonEventFormatter());

/*var httpClient = new HttpClient();
var result = (await httpClient.PostAsync("https://testcloudevents.free.beeceptor.com/my/api/path", content));*/

//Message<string?, byte[]> kafka = cloudEvent.ToKafkaMessage(ContentMode.Binary, new JsonEventFormatter());
        }
    }
}