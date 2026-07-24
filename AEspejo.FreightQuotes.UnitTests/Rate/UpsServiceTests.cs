using System.Net;
using System.Text;
using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Ups;
using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Ups.Request;
using AEspejo.FreightQuotes.CarrierApiClient.Http;
using Moq;

namespace AEspejo.FreightQuotes.UnitTests.Rate;

public class UpsServiceTests
{
    private static UpsService CreateSut(StubHttpMessageHandler handler)
    {
        var factory = new Mock<IHttpClientFactory>();
        factory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(new HttpClient(handler));
        return new UpsService(new ApiCaller(factory.Object));
    }

    private static UpsTokenRequest TokenRequest(string? merchantId = "123456") => new()
    {
        UrlToken = "https://wwwcie.ups.com/security/v1/oauth/token",
        ClientId = "cid",
        ClientSecret = "secret",
        MerchantId = merchantId,
        ApiCallTimeout = 30000,
    };

    [Fact]
    public async Task Token_Success_UsesBasicAuth_AndPostsGrantType()
    {
        var handler = new StubHttpMessageHandler(HttpStatusCode.OK, """{ "access_token": "abc123", "token_type": "Bearer" }""");
        var expectedBasic = Convert.ToBase64String(Encoding.UTF8.GetBytes("cid:secret"));

        var result = await CreateSut(handler).Token(TokenRequest(), CancellationToken.None);

        Assert.Equal("abc123", result.AccessToken);
        Assert.Equal(HttpMethod.Post, handler.Method);
        Assert.Equal("Basic", handler.Authorization!.Scheme);
        Assert.Equal(expectedBasic, handler.Authorization.Parameter);
        Assert.Equal("123456", handler.Headers["x-merchant-id"]);
        Assert.Contains("grant_type=client_credentials", handler.RequestBody);
    }

    [Fact]
    public async Task Token_NoMerchantId_OmitsHeader()
    {
        var handler = new StubHttpMessageHandler(HttpStatusCode.OK, """{ "access_token": "abc" }""");

        await CreateSut(handler).Token(TokenRequest(merchantId: null), CancellationToken.None);

        Assert.False(handler.Headers.ContainsKey("x-merchant-id"));
    }

    [Fact]
    public async Task Token_ErrorEnvelope_ReturnsMessages()
    {
        var handler = new StubHttpMessageHandler(HttpStatusCode.Unauthorized,
            """{ "response": { "errors": [ { "code": "10401", "message": "Invalid credentials" } ] } }""");

        var result = await CreateSut(handler).Token(TokenRequest(), CancellationToken.None);

        Assert.Null(result.AccessToken);
        Assert.Contains("Invalid credentials", result.Messages);
    }

    [Fact]
    public async Task RateAsync_BuildsVersionedUrl_SetsHeaders_AndParses()
    {
        const string body = """{ "RateResponse": { "RatedShipment": [ { "Service": { "Code": "03" } } ] } }""";
        var handler = new StubHttpMessageHandler(HttpStatusCode.OK, body);
        var credentials = new UpsCredentials { RateUrl = "https://wwwcie.ups.com/api/rating", ApiCallTimeout = 30000 };

        var result = await CreateSut(handler).RateAsync(new UpsRateRoot(), "tok-xyz", credentials, CancellationToken.None);

        Assert.NotNull(result.Data);
        Assert.Single(result.Data!.RateResponse!.RatedShipment!);
        Assert.Equal(body, result.RawResponse);
        Assert.Equal("https://wwwcie.ups.com/api/rating/v2409/Rate", handler.RequestUri!.ToString());
        Assert.Equal("Bearer tok-xyz", handler.Headers["Authorization"]);
        Assert.Equal("FreightQuotes", handler.Headers["transactionSrc"]);
        Assert.True(handler.Headers.ContainsKey("transId"));
    }

    [Fact]
    public async Task RateAsync_TrimsTrailingSlashOnBaseUrl()
    {
        var handler = new StubHttpMessageHandler(HttpStatusCode.OK, """{ "RateResponse": { "RatedShipment": [] } }""");
        var credentials = new UpsCredentials { RateUrl = "https://wwwcie.ups.com/api/rating/", ApiCallTimeout = 30000 };

        await CreateSut(handler).RateAsync(new UpsRateRoot(), "tok", credentials, CancellationToken.None);

        Assert.Equal("https://wwwcie.ups.com/api/rating/v2409/Rate", handler.RequestUri!.ToString());
    }
}
