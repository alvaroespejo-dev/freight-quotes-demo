namespace AEspejo.FreightQuotes.CarrierApiClient.Http
{
    /// <summary>
    /// Centralizes the transport concerns shared by every carrier API call: resolving the named
    /// <see cref="System.Net.Http.HttpClient"/>, applying a per-call timeout, sending the request/reading the body,
    /// and JSON (de)serialization. Carrier services keep only their own request building (auth, headers, URL)
    /// and response parsing.
    /// </summary>
    public interface IApiCaller
    {
        /// <summary>
        /// Sends <paramref name="request"/> through the named HttpClient under a per-call timeout and returns the
        /// raw response body. Takes ownership of <paramref name="request"/> (and its content), disposing it.
        /// A genuine caller cancellation and a timeout both surface as <see cref="System.OperationCanceledException"/>.
        /// </summary>
        Task<string> SendAsync(HttpRequestMessage request, string clientName, int? timeoutMs, CancellationToken ct);

        /// <summary>Serializes <paramref name="payload"/> to JSON (nulls omitted).</summary>
        string SerializeJson(object payload);

        /// <summary>Deserializes <paramref name="body"/>, returning null when it is empty or not valid JSON.</summary>
        T? Deserialize<T>(string body) where T : class;
    }
}
