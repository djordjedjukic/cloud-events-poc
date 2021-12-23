using CloudNative.CloudEvents;
using CloudNative.CloudEvents.Http;
using Newtonsoft.Json;
using CloudNative.CloudEvents.NewtonsoftJson;

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

var httpClient = new HttpClient();
var result = (await httpClient.PostAsync("https://testcloudevents.free.beeceptor.com/my/api/path", content));

public record GameResult(string GameId, string PlayerId, int Score);

