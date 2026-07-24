using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Fedex;
using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Fedex.Constants;
using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Fedex.Request;
using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Fedex.Response;
using AEspejo.FreightQuotes.CarrierApiClient.Interfaces.ICarriers;
using AEspejo.FreightQuotes.CarrierApiClient.Rate;
using Moq;

namespace AEspejo.FreightQuotes.UnitTests.Rate;

public class FedexRateClientTests
{
    private readonly Mock<IFedexService> _fedex = new();

    private FedexRateClient CreateSut() => new(_fedex.Object);

    private void SetupToken(string? accessToken, params string[] messages) =>
        _fedex.Setup(s => s.Token(It.IsAny<FedexTokenRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new FedexToken { AccessToken = accessToken, Messages = messages.ToList() });

    private void SetupRate(FedexRateResponse? data) =>
        _fedex.Setup(s => s.RateAsync(It.IsAny<FedexRateRequest>(), It.IsAny<string>(), It.IsAny<FedexCredentials>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new FedexRateResult { Data = data });

    [Fact]
    public async Task GetQuoteAsync_MissingItems_ReturnsErrorQuote_AndSkipsApi()
    {
        var request = RateTestData.ValidRequest();
        request.LineItems.Clear();

        var result = await CreateSut().GetQuoteAsync(request, RateTestData.Carrier("FXFE"), CancellationToken.None);

        var quote = Assert.Single(result);
        Assert.True(quote.HasError);
        Assert.Contains("item", quote.Note);
        _fedex.Verify(s => s.Token(It.IsAny<FedexTokenRequest>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task GetQuoteAsync_TokenFails_ReturnsErrorQuote_AndSkipsRate()
    {
        SetupToken(accessToken: null, "auth failed");

        var result = await CreateSut().GetQuoteAsync(RateTestData.ValidRequest(), RateTestData.Carrier("FXFE"), CancellationToken.None);

        var quote = Assert.Single(result);
        Assert.True(quote.HasError);
        Assert.Contains("auth failed", quote.Note);
        _fedex.Verify(s => s.RateAsync(It.IsAny<FedexRateRequest>(), It.IsAny<string>(), It.IsAny<FedexCredentials>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task GetQuoteAsync_MapsAccountRatedShipment_ExcludingFuel()
    {
        SetupToken("tok");
        SetupRate(new FedexRateResponse
        {
            Output = new RatesOutput
            {
                RateReplyDetails =
                [
                    new RateReplyDetail
                    {
                        ServiceType = FedexApiConstants.ServiceTypePriority,
                        ServiceName = "FedEx Freight Priority",
                        Commit = new Commit { TransitDays = new TransitDays { MinimumTransitTime = "TWO_DAYS" } },
                        RatedShipmentDetails =
                        [
                            new RatedShipmentDetail
                            {
                                RateType = FedexApiConstants.RateRequestTypeAccount,
                                QuoteNumber = "Q-100",
                                TotalNetCharge = 200.00m,
                                ShipmentRateDetail = new ShipmentRateDetail
                                {
                                    SurCharges =
                                    [
                                        new SurCharge { Type = "OTHER", Description = "Liftgate", Amount = 25.00m },
                                        new SurCharge { Type = FedexApiConstants.SurchargeTypeFuel, Description = "Fuel", Amount = 10.00m },
                                    ],
                                },
                            },
                        ],
                    },
                ],
            },
        });

        var result = await CreateSut().GetQuoteAsync(RateTestData.ValidRequest(), RateTestData.Carrier("FXFE"), CancellationToken.None);

        var quote = Assert.Single(result);
        Assert.False(quote.HasError);
        Assert.Equal("FedEx Freight Priority", quote.ServiceLevel);
        Assert.Equal("Q-100", quote.QuoteNumber);
        Assert.Equal(200.00m, quote.TotalCharge);
        Assert.Equal(25.00m, quote.AccessorialCharge);
        Assert.Equal(175.00m, quote.BaseCharge);
        Assert.Equal(2, quote.TransitDays);
        Assert.Equal("Liftgate", Assert.Single(quote.Accessorial).Name);
    }

    [Fact]
    public async Task GetQuoteAsync_ResponseErrors_ReturnErrorQuote()
    {
        SetupToken("tok");
        SetupRate(new FedexRateResponse
        {
            Errors = [new Error { Code = "RATE.POSTAL.INVALID", Message = "Invalid postal code" }],
        });

        var result = await CreateSut().GetQuoteAsync(RateTestData.ValidRequest(), RateTestData.Carrier("FXFE"), CancellationToken.None);

        var quote = Assert.Single(result);
        Assert.True(quote.HasError);
        Assert.Contains("Invalid postal code", quote.Note);
    }
}
