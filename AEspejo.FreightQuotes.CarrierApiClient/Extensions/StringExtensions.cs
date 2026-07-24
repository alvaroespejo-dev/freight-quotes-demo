namespace AEspejo.FreightQuotes.CarrierApiClient.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Returns the string truncated to <paramref name="maxLength"/>, or an empty string when null/empty.
        /// </summary>
        public static string TruncateOrEmpty(this string? value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            return value.Length <= maxLength ? value : value[..maxLength];
        }
    }
}
