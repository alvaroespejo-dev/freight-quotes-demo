using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Ups;
using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Ups.Request;
using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Ups.Response;
using AEspejo.FreightQuotes.CarrierApiClient.Interfaces.ICarriers;
using AEspejo.FreightQuotes.CarrierApiClient.Rate;
using AEspejo.FreightQuotes.Shared.Dtos.Rate.RateRequest;
using Moq;

namespace AEspejo.FreightQuotes.UnitTests.Rate;

public class UpsRateClientTests
{
    private readonly Mock<IUpsService> _ups = new();

    private UpsRateClient CreateSut() => new(_ups.Object);

    private void SetupToken(string? accessToken, params string[] messages) =>
        _ups.Setup(s => s.Token(It.IsAny<UpsTokenRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new UpsToken { AccessToken = accessToken, Messages = messages.ToList() });

    private void SetupRate(UpsRateResponseRoot? data) =>
        _ups.Setup(s => s.RateAsync(It.IsAny<UpsRateRoot>(), It.IsAny<string>(), It.IsAny<UpsCredentials>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new UpsRateResult { Data = data });

    private static UpsMoney Money(string value) => new() { CurrencyCode = "USD", MonetaryValue = value };

    private static UpsRateResponseRoot RootWith(params UpsRatedShipment[] shipments) =>
        new() { RateResponse = new UpsRateResponse { RatedShipment = shipments } };

    [Fact]
    public async Task GetQuoteAsync_MissingOrigin_ReturnsErrorQuote_AndSkipsApi()
    {
        var request = RateTestData.ValidRequest();
        request.OriginAddress = null!;

        var result = await CreateSut().GetQuoteAsync(request, RateTestData.Carrier("UPS"), CancellationToken.None);

        var quote = Assert.Single(result);
        Assert.True(quote.HasError);
        Assert.Contains("Origin", quote.Note);
        _ups.Verify(s => s.Token(It.IsAny<UpsTokenRequest>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task GetQuoteAsync_TokenFails_ReturnsErrorQuote_AndSkipsRate()
    {
        SetupToken(accessToken: null, "invalid client");

        var result = await CreateSut().GetQuoteAsync(RateTestData.ValidRequest(), RateTestData.Carrier("UPS"), CancellationToken.None);

        var quote = Assert.Single(result);
        Assert.True(quote.HasError);
        Assert.Contains("invalid client", quote.Note);
        _ups.Verify(s => s.RateAsync(It.IsAny<UpsRateRoot>(), It.IsAny<string>(), It.IsAny<UpsCredentials>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task GetQuoteAsync_MapsRatedShipment()
    {
        SetupToken("tok");
        SetupRate(RootWith(new UpsRatedShipment
        {
            Service = new UpsCodeDescription { Code = "03" },
            TransportationCharges = Money("100.00"),
            ServiceOptionsCharges = Money("20.00"),
            TotalCharges = Money("120.00"),
            GuaranteedDelivery = new UpsGuaranteedDelivery { BusinessDaysInTransit = "3" },
            ItemizedCharges =
            [
                new UpsItemizedCharge { Code = "110", Description = "Residential", MonetaryValue = "15.00" },
                new UpsItemizedCharge { Code = "375", Description = "Fuel Surcharge", MonetaryValue = "5.00" },
            ],
        }));

        var result = await CreateSut().GetQuoteAsync(RateTestData.ValidRequest(), RateTestData.Carrier("UPS"), CancellationToken.None);

        var quote = Assert.Single(result);
        Assert.False(quote.HasError);
        Assert.Equal("UPS Ground", quote.ServiceLevel);
        Assert.Equal(120.00m, quote.TotalCharge);
        Assert.Equal(20.00m, quote.AccessorialCharge);
        Assert.Equal(100.00m, quote.BaseCharge);
        Assert.Equal(3, quote.TransitDays);
        // Fuel (code 375) excluded from the accessorial breakdown.
        var accessorial = Assert.Single(quote.Accessorial);
        Assert.Equal("Residential", accessorial.Name);
        Assert.Equal(15.00m, accessorial.Cost);
    }

    [Fact]
    public async Task GetQuoteAsync_PrefersNegotiatedRate_OverPublishedTotal()
    {
        SetupToken("tok");
        SetupRate(RootWith(new UpsRatedShipment
        {
            Service = new UpsCodeDescription { Code = "03" },
            TransportationCharges = Money("100.00"),
            TotalCharges = Money("120.00"),
            NegotiatedRateCharges = new UpsNegotiatedRateCharges { TotalCharge = Money("90.00") },
        }));

        var result = await CreateSut().GetQuoteAsync(RateTestData.ValidRequest(), RateTestData.Carrier("UPS"), CancellationToken.None);

        Assert.Equal(90.00m, Assert.Single(result).TotalCharge);
    }

    [Fact]
    public async Task GetQuoteAsync_ErrorEnvelope_ReturnsErrorQuote()
    {
        SetupToken("tok");
        SetupRate(new UpsRateResponseRoot
        {
            ErrorEnvelope = new UpsErrorEnvelope
            {
                Errors = [new UpsError { Code = "111", Message = "Missing/Invalid ship to postal code" }],
            },
        });

        var result = await CreateSut().GetQuoteAsync(RateTestData.ValidRequest(), RateTestData.Carrier("UPS"), CancellationToken.None);

        var quote = Assert.Single(result);
        Assert.True(quote.HasError);
        Assert.Contains("postal code", quote.Note);
    }

    [Fact]
    public async Task GetQuoteAsync_NoRatedShipments_ReturnsErrorQuote()
    {
        SetupToken("tok");
        SetupRate(RootWith());

        var result = await CreateSut().GetQuoteAsync(RateTestData.ValidRequest(), RateTestData.Carrier("UPS"), CancellationToken.None);

        Assert.True(Assert.Single(result).HasError);
    }

    [Fact]
    public async Task GetQuoteAsync_BuildsRequest_WithShipperAccountAndPackages()
    {
        UpsRateRoot? captured = null;
        SetupToken("tok");
        _ups.Setup(s => s.RateAsync(It.IsAny<UpsRateRoot>(), It.IsAny<string>(), It.IsAny<UpsCredentials>(), It.IsAny<CancellationToken>()))
            .Callback<UpsRateRoot, string, UpsCredentials, CancellationToken>((req, _, _, _) => captured = req)
            .ReturnsAsync(new UpsRateResult { Data = RootWith() });

        await CreateSut().GetQuoteAsync(RateTestData.ValidRequest(), RateTestData.Carrier("UPS", account: "ACC123"), CancellationToken.None);

        Assert.NotNull(captured);
        var shipment = captured!.RateRequest.Shipment;
        Assert.Equal("ACC123", shipment.Shipper.ShipperNumber);
        Assert.Equal("ACC123", shipment.PaymentDetails!.ShipmentCharge[0].BillShipper!.AccountNumber);
        Assert.Equal("03", shipment.Service!.Code);
        Assert.Single(shipment.Package);
        Assert.Equal("30005", shipment.ShipTo.Address.PostalCode);
        Assert.Equal("21093", shipment.ShipFrom.Address.PostalCode);
        Assert.Equal("100", shipment.Package[0].PackageWeight!.Weight);
    }
}
