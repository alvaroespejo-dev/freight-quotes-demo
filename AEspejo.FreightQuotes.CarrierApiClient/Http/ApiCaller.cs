using Newtonsoft.Json;

namespace AEspejo.FreightQuotes.CarrierApiClient.Http
{
    /// <summary>
    /// Default <see cref="IApiCaller"/>. Wraps <see cref="IHttpClientFactory"/> so client lifetime/handlers are
    /// managed by DI, and applies a per-call timeout via a linked <see cref="CancellationTokenSource"/>.
    /// </summary>
    public class ApiCaller(IHttpClientFactory httpClientFactory) : IApiCaller
    {
        private static readonly JsonSerializerSettings _jsonSettings = new()
        {
            NullValueHandling = NullValueHandling.Ignore,
        };

        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        public async Task<string> SendAsync(HttpRequestMessage request, string clientName, int? timeoutMs, CancellationToken ct)
        {
            using (request)
            using (var timeoutCts = CreateTimeoutScope(ct, timeoutMs))
            {
                var httpClient = _httpClientFactory.CreateClient(clientName);

                using var response = await httpClient.SendAsync(request, timeoutCts.Token);
                return await response.Content.ReadAsStringAsync(timeoutCts.Token);
            }
        }

        public string SerializeJson(object payload) => JsonConvert.SerializeObject(payload, _jsonSettings);

        public T? Deserialize<T>(string body) where T : class
        {
            if (string.IsNullOrWhiteSpace(body))
            {
                return null;
            }

            try
            {
                return JsonConvert.DeserializeObject<T>(body);
            }
            catch (JsonException)
            {
                return null;
            }
        }

        private static CancellationTokenSource CreateTimeoutScope(CancellationToken ct, int? timeoutMs)
        {
            var cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
            if (timeoutMs is > 0)
            {
                cts.CancelAfter(timeoutMs.Value);
            }

            return cts;
        }
    }
}
