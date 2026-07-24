using System.Globalization;

namespace AEspejo.FreightQuotes.CarrierApiClient.Carriers.Ups
{
    /// <summary>
    /// Translates UPS service codes to display names and parses the string-typed
    /// numeric fields UPS returns (money, transit days).
    /// </summary>
    internal static class UpsMappings
    {
        // UPS Service.Code -> human-readable service level (US domestic + common international).
        private static readonly Dictionary<string, string> _serviceNameByCode = new()
        {
            ["01"] = "UPS Next Day Air",
            ["02"] = "UPS 2nd Day Air",
            ["03"] = "UPS Ground",
            ["07"] = "UPS Worldwide Express",
            ["08"] = "UPS Worldwide Expedited",
            ["11"] = "UPS Standard",
            ["12"] = "UPS 3 Day Select",
            ["13"] = "UPS Next Day Air Saver",
            ["14"] = "UPS Next Day Air Early",
            ["54"] = "UPS Worldwide Express Plus",
            ["59"] = "UPS 2nd Day Air A.M.",
            ["65"] = "UPS Worldwide Saver",
        };

        public static string ServiceName(string? code)
        {
            if (!string.IsNullOrWhiteSpace(code) && _serviceNameByCode.TryGetValue(code.Trim(), out var name))
            {
                return name;
            }

            return string.IsNullOrWhiteSpace(code) ? string.Empty : $"UPS {code}";
        }

        public static decimal ParseMoney(string? value)
            => decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result) ? result : 0m;

        public static int ParseDays(string? value)
            => int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var result) ? result : 0;
    }
}
