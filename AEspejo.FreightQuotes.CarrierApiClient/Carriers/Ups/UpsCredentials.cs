using AEspejo.FreightQuotes.Domain.Entities;
using AEspejo.FreightQuotes.Shared.Constants;

namespace AEspejo.FreightQuotes.CarrierApiClient.Carriers.Ups
{
    /// <summary>
    /// UPS credentials/endpoints resolved from the carrier's <see cref="CarrierSetting"/> rows,
    /// matching each value by its (SettingType, CarrierSettingType) constant ids seeded in the database.
    /// Mirrors <c>FedexCredentials</c>.
    /// </summary>
    public class UpsCredentials
    {
        /// <summary>Base Rating URL, e.g. <c>https://wwwcie.ups.com/api/rating</c>. The version and request option are appended by the service.</summary>
        public string RateUrl { get; set; } = string.Empty;

        /// <summary>OAuth token URL, e.g. <c>https://wwwcie.ups.com/security/v1/oauth/token</c>.</summary>
        public string TokenUrl { get; set; } = string.Empty;

        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;

        /// <summary>UPS account (shipper) number used for ShipperNumber and BillShipper.</summary>
        public string Account { get; set; } = string.Empty;

        public int ApiCallTimeout { get; set; } = 30000;

        public static UpsCredentials FromCarrier(Carrier carrier)
        {
            string Value(long settingTypeId, long carrierSettingTypeId) =>
                carrier.Settings.FirstOrDefault(s =>
                    s.IsActive &&
                    s.SettingTypeId == settingTypeId &&
                    s.CarrierSettingTypeId == carrierSettingTypeId)?.Value ?? string.Empty;

            return new UpsCredentials
            {
                RateUrl = Value(SettingTypeConstant.Rating, CarrierSettingTypeConstant.Url),
                TokenUrl = Value(SettingTypeConstant.Authentication, CarrierSettingTypeConstant.Url),
                ClientId = Value(SettingTypeConstant.Authentication, CarrierSettingTypeConstant.ClientId),
                ClientSecret = Value(SettingTypeConstant.Authentication, CarrierSettingTypeConstant.ClientSecret),
                Account = Value(SettingTypeConstant.Rating, CarrierSettingTypeConstant.Account),
            };
        }
    }
}
