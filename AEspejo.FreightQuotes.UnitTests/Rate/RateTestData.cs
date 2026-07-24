using AEspejo.FreightQuotes.Domain.Entities;
using AEspejo.FreightQuotes.Shared.Constants;
using AEspejo.FreightQuotes.Shared.Dtos.Rate.RateRequest;

namespace AEspejo.FreightQuotes.UnitTests.Rate;

/// <summary>
/// Shared builders for the rate-client unit tests (carrier + a valid quote request).
/// </summary>
internal static class RateTestData
{
    public static Carrier Carrier(string scac, bool mock = false, string account = "ACC123") => new()
    {
        Id = 10,
        Name = scac,
        Scac = scac,
        IsMockMode = mock,
        IsActive = true,
        Settings = new List<CarrierSetting>
        {
            Setting(SettingTypeConstant.Rating, CarrierSettingTypeConstant.Account, account),
            Setting(SettingTypeConstant.Rating, CarrierSettingTypeConstant.Url, "https://rate.test/api/rating"),
            Setting(SettingTypeConstant.Authentication, CarrierSettingTypeConstant.Url, "https://token.test/oauth"),
            Setting(SettingTypeConstant.Authentication, CarrierSettingTypeConstant.ClientId, "cid"),
            Setting(SettingTypeConstant.Authentication, CarrierSettingTypeConstant.ClientSecret, "secret"),
        },
    };

    private static CarrierSetting Setting(long settingTypeId, long carrierSettingTypeId, string value) => new()
    {
        IsActive = true,
        SettingTypeId = settingTypeId,
        CarrierSettingTypeId = carrierSettingTypeId,
        Value = value,
    };

    public static RateQuoteRequest ValidRequest() => new()
    {
        RequestId = "req-1",
        ShipDate = new DateTime(2026, 1, 15),
        OriginAddress = Address("Timonium", "MD", "21093"),
        DestinationAddress = Address("Alpharetta", "GA", "30005"),
        Accessorials = new List<RateAccessorialRequest>(),
        LineItems = new List<RateQuoteLineItem>
        {
            new()
            {
                Qty = 1,
                ShipQty = 1,
                Weight = 100m,
                Length = 48,
                Width = 40,
                Height = 40,
                FreightClass = "50",
                ShippingUnitCode = "Pallets",
                Description = "Widget",
            },
        },
    };

    private static RateQuoteAddress Address(string city, string state, string zip) => new()
    {
        Address1 = "123 Main St",
        City = city,
        StateCode = state,
        CountryCode = "US",
        Zip = zip,
        Accessorials = new List<RateAccessorialRequest>(),
    };
}
