using System.Net;
using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Fedex;
using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Fedex.Request;
using AEspejo.FreightQuotes.CarrierApiClient.Http;
using Moq;

namespace AEspejo.FreightQuotes.UnitTests.Rate;

public class FedexServiceTests
{
    private static FedexService CreateSut(StubHttpMessageHandler handler)
    {
        var factory = new Mock<IHttpClientFactory>();
        factory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(new HttpClient(handler));
        return new FedexService(new ApiCaller(factory.Object));
    }

    private static FedexTokenRequest TokenRequest() => new()
    {
        UrlToken = "https://token.fedex.test/oauth/token",
        ClientId = "cid",
        ClientSecret = "secret",
        ApiCallTimeout = 30000,
    };

    [Fact]
    public async Task Token_Success_ReturnsAccessToken_AndPostsCredentials()
    {
        var handler = new StubHttpMessageHandler(HttpStatusCode.OK, """{ "access_token": "abc123" }""");

        var result = await CreateSut(handler).Token(TokenRequest(), CancellationToken.None);

        Assert.Equal("abc123", result.AccessToken);
        Assert.Equal(HttpMethod.Post, handler.Method);
        Assert.Equal("https://token.fedex.test/oauth/token", handler.RequestUri!.ToString());
        Assert.Contains("grant_type=client_credentials", handler.RequestBody);
        Assert.Contains("client_id=cid", handler.RequestBody);
        Assert.Contains("client_secret=secret", handler.RequestBody);
    }

    [Fact]
    public async Task Token_ErrorPayload_ReturnsMessages()
    {
        var handler = new StubHttpMessageHandler(HttpStatusCode.BadRequest,
            """{ "errors": [ { "code": "AUTH.ERROR", "message": "invalid client" } ] }""");

        var result = await CreateSut(handler).Token(TokenRequest(), CancellationToken.None);

        Assert.Null(result.AccessToken);
        Assert.Contains("invalid client", result.Messages);
    }

    [Fact]
    public async Task Token_EmptyBody_ReturnsGenericMessage()
    {
        var handler = new StubHttpMessageHandler(HttpStatusCode.OK, "{}");

        var result = await CreateSut(handler).Token(TokenRequest(), CancellationToken.None);

        Assert.Null(result.AccessToken);
        Assert.Single(result.Messages);
    }

    [Fact]
    public async Task RateAsync_ParsesResponse_SetsBearer_AndKeepsRaw()
    {
        const string body = """{ "output": { "rateReplyDetails": [ { "serviceType": "FEDEX_FREIGHT_PRIORITY" } ] } }""";
        var handler = new StubHttpMessageHandler(HttpStatusCode.OK, body);
        var credentials = new FedexCredentials { RateUrl = "https://rate.fedex.test/rate/v1/freight/rates/quotes", ApiCallTimeout = 30000 };

        var result = await CreateSut(handler).RateAsync(new FedexRateRequest(), "tok-xyz", credentials, CancellationToken.None);

        Assert.NotNull(result.Data);
        Assert.Single(result.Data!.Output!.RateReplyDetails!);
        Assert.Equal(body, result.RawResponse);
        Assert.Equal(credentials.RateUrl, handler.RequestUri!.ToString());
        Assert.Equal("Bearer tok-xyz", handler.Headers["Authorization"]);
    }

    [Fact]
    public async Task RateAsync_InvalidJson_ReturnsNullData_ButKeepsRaw()
    {
        const string body = "not-json";
        var handler = new StubHttpMessageHandler(HttpStatusCode.OK, body);
        var credentials = new FedexCredentials { RateUrl = "https://rate.fedex.test/quotes", ApiCallTimeout = 30000 };

        var result = await CreateSut(handler).RateAsync(new FedexRateRequest(), "tok", credentials, CancellationToken.None);

        Assert.Null(result.Data);
        Assert.Equal(body, result.RawResponse);
    }
}
