using AEspejo.FreightQuotes.CarrierApiClient.Carriers.Fedex.Request;

namespace AEspejo.FreightQuotes.CarrierApiClient.Carriers.Fedex
{
    /// <summary>
    /// Translates this system's resolved codes/values into the enum literals FedEx expects,
    /// and parses FedEx literal transit times back into integers.
    /// </summary>
    internal static class FedexMappings
    {
        private static readonly Dictionary<string, FedexFreightClass> _freightClassByValue = new()
        {
            ["50"] = FedexFreightClass.CLASS_050,
            ["55"] = FedexFreightClass.CLASS_055,
            ["60"] = FedexFreightClass.CLASS_060,
            ["65"] = FedexFreightClass.CLASS_065,
            ["70"] = FedexFreightClass.CLASS_070,
            ["77.5"] = FedexFreightClass.CLASS_077_5,
            ["85"] = FedexFreightClass.CLASS_085,
            ["92.5"] = FedexFreightClass.CLASS_092_5,
            ["100"] = FedexFreightClass.CLASS_100,
            ["110"] = FedexFreightClass.CLASS_110,
            ["125"] = FedexFreightClass.CLASS_125,
            ["150"] = FedexFreightClass.CLASS_150,
            ["175"] = FedexFreightClass.CLASS_175,
            ["200"] = FedexFreightClass.CLASS_200,
            ["250"] = FedexFreightClass.CLASS_250,
            ["300"] = FedexFreightClass.CLASS_300,
            ["400"] = FedexFreightClass.CLASS_400,
            ["500"] = FedexFreightClass.CLASS_500,
        };

        // Keyed by the ShippingUnits Constant.Code values seeded in the database.
        private static readonly Dictionary<string, SubPackageTypes> _subPackageByCode = new(StringComparer.OrdinalIgnoreCase)
        {
            ["Bags"] = SubPackageTypes.BAG,
            ["Barrels"] = SubPackageTypes.BARREL,
            ["Baskets"] = SubPackageTypes.BASKET,
            ["Boxes"] = SubPackageTypes.BOX,
            ["Buckets"] = SubPackageTypes.BUCKET,
            ["Bundles"] = SubPackageTypes.BUNDLE,
            ["Cartons"] = SubPackageTypes.CARTON,
            ["Cases"] = SubPackageTypes.CASE,
            ["Crate"] = SubPackageTypes.CRATE,
            ["Drums"] = SubPackageTypes.DRUM,
            ["Hampers"] = SubPackageTypes.HAMPER,
            ["Packages"] = SubPackageTypes.PACKAGE,
            ["Pails"] = SubPackageTypes.PAIL,
            ["Pallets"] = SubPackageTypes.PALLET,
            ["Pieces"] = SubPackageTypes.PIECE,
            ["Reels"] = SubPackageTypes.REEL,
            ["Rolls"] = SubPackageTypes.ROLL,
            ["Skid"] = SubPackageTypes.SKID,
            ["Tanks"] = SubPackageTypes.TANK,
            ["Totes"] = SubPackageTypes.TOTEBIN,
            ["Tubes"] = SubPackageTypes.TUBE,
        };

        private static readonly Dictionary<string, int> _numberByLiteral = new(StringComparer.OrdinalIgnoreCase)
        {
            ["ZERO"] = 0,
            ["ONE"] = 1,
            ["TWO"] = 2,
            ["THREE"] = 3,
            ["FOUR"] = 4,
            ["FIVE"] = 5,
            ["SIX"] = 6,
            ["SEVEN"] = 7,
            ["EIGHT"] = 8,
            ["NINE"] = 9,
            ["TEN"] = 10,
            ["ELEVEN"] = 11,
            ["TWELVE"] = 12,
            ["THIRTEEN"] = 13,
            ["FOURTEEN"] = 14,
            ["FIFTEEN"] = 15,
            ["SIXTEEN"] = 16,
            ["SEVENTEEN"] = 17,
            ["EIGHTEEN"] = 18,
            ["NINETEEN"] = 19,
            ["TWENTY"] = 20,
        };

        public static string FreightClass(string? value)
        {
            if (!string.IsNullOrWhiteSpace(value) && _freightClassByValue.TryGetValue(value.Trim(), out var freightClass))
            {
                return freightClass.ToString();
            }

            return string.Empty;
        }

        public static string SubPackagingType(string? code)
        {
            if (!string.IsNullOrWhiteSpace(code) && _subPackageByCode.TryGetValue(code.Trim(), out var subPackage))
            {
                return subPackage.ToString();
            }

            // Freight moves on pallets by default when the shipping unit is unknown.
            return SubPackageTypes.PALLET.ToString();
        }

        /// <summary>
        /// Parses a FedEx transit literal such as "TWO_DAYS" into its number of days (0 when not parseable).
        /// </summary>
        public static int TransitDaysFromLiteral(string? literal)
        {
            if (string.IsNullOrWhiteSpace(literal))
            {
                return 0;
            }

            var head = literal.Split('_').FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(head) && _numberByLiteral.TryGetValue(head, out var days))
            {
                return days;
            }

            return 0;
        }
    }
}
