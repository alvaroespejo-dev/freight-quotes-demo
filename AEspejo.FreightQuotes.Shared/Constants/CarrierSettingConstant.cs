namespace AEspejo.FreightQuotes.Shared.Constants
{
    /// <summary>
    /// Constant ids seeded for the "SettingType" ConstantType (ConstantTypeId = 7).
    /// </summary>
    public static class SettingTypeConstant
    {
        public const long Rating = 99;
        public const long Authentication = 100;
    }

    /// <summary>
    /// Constant ids seeded for the "CarrierSettingType" ConstantType (ConstantTypeId = 8).
    /// </summary>
    public static class CarrierSettingTypeConstant
    {
        public const long Url = 101;
        public const long ClientId = 102;
        public const long ClientSecret = 103;
        public const long UserName = 104;
        public const long Password = 105;
        public const long ApiKey = 106;
        public const long Account = 107;
        public const long AccountSecundary = 108;
    }

    /// <summary>
    /// Constant ids seeded for the "Terms" ConstantType (ConstantTypeId = 9).
    /// </summary>
    public static class TermsConstant
    {
        public const long Collect = 109;
        public const long Prepaid = 110;
        public const long ThirdParty = 111;
    }

    /// <summary>
    /// Constant ids seeded for the "Role" ConstantType (ConstantTypeId = 10).
    /// </summary>
    public static class RoleConstant
    {
        public const long Consignee = 112;
        public const long Shipper = 113;
        public const long ThirdParty = 114;
    }
}
