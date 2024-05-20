using Microsoft.AspNetCore.SignalR;

namespace LinkPoint.Business.Hubs
{
    public class ChatHub:Hub
    {
        public async Task SendMessageAsync(string Username, string Message)
        {
            await Clients.All.SendAsync("ReceiveMessage",Username, Message);
        }
    }
}
