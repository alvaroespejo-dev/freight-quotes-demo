using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace AEspejo.FreightQuotes.Api.Hubs
{
    public class RateQuoteHub : Hub
    {
        public Task JoinRequest(string requestId)
        => Groups.AddToGroupAsync(Context.ConnectionId, requestId);
    }
}
