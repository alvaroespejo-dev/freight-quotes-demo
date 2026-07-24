using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace AEspejo.FreightQuotes.UnitTests.Rate;

/// <summary>
/// Fake <see cref="HttpMessageHandler"/> that returns a canned JSON response and snapshots the outgoing
/// request (method, URI, headers, body) so tests can assert what the carrier service actually sent —
/// the real request/response are disposed by the service before the test regains control.
/// </summary>
internal sealed class StubHttpMessageHandler(HttpStatusCode status, string body) : HttpMessageHandler
{
    public HttpMethod? Method { get; private set; }
    public Uri? RequestUri { get; private set; }
    public AuthenticationHeaderValue? Authorization { get; private set; }
    public Dictionary<string, string> Headers { get; } = new(StringComparer.OrdinalIgnoreCase);
    public string? RequestBody { get; private set; }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Method = request.Method;
        RequestUri = request.RequestUri;
        Authorization = request.Headers.Authorization;

        foreach (var header in request.Headers)
        {
            Headers[header.Key] = string.Join(",", header.Value);
        }

        if (request.Content is not null)
        {
            RequestBody = await request.Content.ReadAsStringAsync(cancellationToken);
        }

        return new HttpResponseMessage(status)
        {
            Content = new StringContent(body, Encoding.UTF8, "application/json"),
        };
    }
}
