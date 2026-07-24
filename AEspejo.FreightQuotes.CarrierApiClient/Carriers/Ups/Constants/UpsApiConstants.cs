namespace AEspejo.FreightQuotes.CarrierApiClient.Carriers.Ups.Constants
{
    /// <summary>
    /// Literal values UPS expects in the Rating API request (service/packaging codes, units, etc.).
    /// </summary>
    public static class UpsApiConstants
    {
        // Path params for POST /rating/{version}/{requestoption}.
        public const string RatingVersion = "v2409";
        public const string RequestOptionRate = "Rate";
        public const string RequestOptionShop = "Shop";
        public const string RequestOptionRateTimeInTransit = "Ratetimeintransit";

        public const string AdditionalInfoTimeInTransit = "timeintransit";

        // Header identifying the calling application (max 512).
        public const string TransactionSource = "FreightQuotes";

        // Service.Code: default small-package service.
        public const string ServiceGround = "03";

        // PackagingType.Code: "02" = customer supplied packaging.
        public const string PackagingTypeCustomer = "02";

        // PaymentDetails.ShipmentCharge.Type: "01" = transportation charges billed to the shipper.
        public const string PaymentTypeBillShipper = "01";

        public const string UnitInches = "IN";
        public const string UnitInchesDescription = "Inches";
        public const string UnitPounds = "LBS";
        public const string UnitPoundsDescription = "Pounds";

        public const string CurrencyUsd = "USD";

        // ItemizedCharges code for the fuel surcharge, excluded from the accessorial breakdown (matches FedEx).
        public const string ItemizedFuelSurchargeCode = "375";
    }
}
