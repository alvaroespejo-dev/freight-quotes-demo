using System.Net;
using AEspejo.FreightQuotes.CarrierApiClient.Http;
using Moq;
using Newtonsoft.Json;

namespace AEspejo.FreightQuotes.UnitTests.Rate;

public class ApiCallerTests
{
    private sealed class Sample
    {
        [JsonProperty("value")]
        public string? Value { get; set; }
    }

    private static ApiCaller CreateSut(StubHttpMessageHandler handler)
    {
        var factory = new Mock<IHttpClientFactory>();
        factory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(new HttpClient(handler));
        return new ApiCaller(factory.Object);
    }

    [Fact]
    public async Task SendAsync_ReturnsBody_AndSendsRequest()
    {
        var handler = new StubHttpMessageHandler(HttpStatusCode.OK, """{ "value": "ok" }""");

        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.test/resource")
        {
            Content = new StringContent("payload"),
        };
        var body = await CreateSut(handler).SendAsync(request, "any-client", timeoutMs: 30000, CancellationToken.None);

        Assert.Contains("ok", body);
        Assert.Equal(HttpMethod.Post, handler.Method);
        Assert.Equal("https://api.test/resource", handler.RequestUri!.ToString());
        Assert.Equal("payload", handler.RequestBody);
    }

    [Fact]
    public async Task SendAsync_DisposesRequest()
    {
        var handler = new StubHttpMessageHandler(HttpStatusCode.OK, "{}");
        var request = new HttpRequestMessage(HttpMethod.Get, "https://api.test/x");

        await CreateSut(handler).SendAsync(request, "any-client", timeoutMs: null, CancellationToken.None);

        // A disposed HttpRequestMessage throws when a property is set afterwards.
        Assert.Throws<ObjectDisposedException>(() => request.Method = HttpMethod.Post);
    }

    [Fact]
    public void Deserialize_InvalidOrEmpty_ReturnsNull()
    {
        var sut = CreateSut(new StubHttpMessageHandler(HttpStatusCode.OK, "{}"));

        Assert.Null(sut.Deserialize<Sample>("not-json"));
        Assert.Null(sut.Deserialize<Sample>(""));
        Assert.Equal("x", sut.Deserialize<Sample>("""{ "value": "x" }""")!.Value);
    }

    [Fact]
    public void SerializeJson_OmitsNulls()
    {
        var sut = CreateSut(new StubHttpMessageHandler(HttpStatusCode.OK, "{}"));

        var json = sut.SerializeJson(new Sample { Value = null });

        Assert.DoesNotContain("value", json);
    }
}
