namespace AEspejo.FreightQuotes.Shared.Dtos.Common
{
    public class SearchListResponse<T>
    {
        public int TotalItems { get; set; }
        public IReadOnlyList<T> Items { get; set; }

        public SearchListResponse()
        {
            Items = [];
        }

        public SearchListResponse(int totalItems, IReadOnlyList<T> items)
        {
            TotalItems = totalItems;
            Items = items ?? [];
        }
    }
}