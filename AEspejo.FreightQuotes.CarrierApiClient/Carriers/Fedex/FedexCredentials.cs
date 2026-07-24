using AEspejo.FreightQuotes.Domain.Entities;
using AEspejo.FreightQuotes.Shared.Constants;

namespace AEspejo.FreightQuotes.CarrierApiClient.Carriers.Fedex
{
    /// <summary>
    /// FedEx credentials/endpoints resolved from the carrier's <see cref="CarrierSetting"/> rows.
    /// </summary>
    public class FedexCredentials
    {
        public string RateUrl { get; set; } = string.Empty;
        public string TokenUrl { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public string AccountSecundary { get; set; } = string.Empty;
        public string Account { get; set; } = string.Empty;
        public int ApiCallTimeout { get; set; } = 30000;

        /// <summary>
        /// Builds the credentials from the carrier settings, matching each value by its
        /// (SettingType, CarrierSettingType) constant ids seeded in the database.
        /// </summary>
        public static FedexCredentials FromCarrier(Carrier carrier)
        {
            string Value(long settingTypeId, long carrierSettingTypeId) =>
                carrier.Settings.FirstOrDefault(s =>
                    s.IsActive &&
                    s.SettingTypeId == settingTypeId &&
                    s.CarrierSettingTypeId == carrierSettingTypeId)?.Value ?? string.Empty;

            var credentials = new FedexCredentials
            {
                RateUrl = Value(SettingTypeConstant.Rating, CarrierSettingTypeConstant.Url),
                TokenUrl = Value(SettingTypeConstant.Authentication, CarrierSettingTypeConstant.Url),
                ClientId = Value(SettingTypeConstant.Authentication, CarrierSettingTypeConstant.ClientId),
                ClientSecret = Value(SettingTypeConstant.Authentication, CarrierSettingTypeConstant.ClientSecret),
                Account = Value(SettingTypeConstant.Rating, CarrierSettingTypeConstant.Account),
                AccountSecundary = Value(SettingTypeConstant.Rating, CarrierSettingTypeConstant.AccountSecundary),
            };

            // FedEx accepts a single account for both express and freight billing when only one is configured.
            credentials.Account = ResolveAccount(credentials.Account, credentials.AccountSecundary);
            credentials.AccountSecundary = ResolveAccount(credentials.AccountSecundary, credentials.Account);

            return credentials;
        }

        private static string ResolveAccount(string primary, string fallback)
            => string.IsNullOrWhiteSpace(primary) ? fallback : primary;
    }
}
