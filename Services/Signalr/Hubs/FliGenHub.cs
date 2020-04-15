using FliGen.Common.Authentication;
using FliGen.Services.Signalr.Framework;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace FliGen.Services.Signalr.Hubs
{
    public class FliGenHub : Hub
    {
        //private readonly IJwtHandler _jwtHandler;

        public FliGenHub(/*IJwtHandler jwtHandler*/)
        {
            //_jwtHandler = jwtHandler;
        }

        public async Task InitializeAsync(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                await DisconnectAsync();
            }
            try
            {
                var payload = token; //_jwtHandler.GetTokenPayload(token); //todo:: remove comments 
                if (payload == null)
                {
                    await DisconnectAsync();

                    return;
                }
                var group = Guid.Parse(payload/*.Subject*/).ToUserGroup();
                await Groups.AddToGroupAsync(Context.ConnectionId, group);
                await ConnectAsync();
            }
            catch
            {
                await DisconnectAsync();
            }
        }

        private async Task ConnectAsync()
        {
            await Clients.Client(Context.ConnectionId).SendAsync("connected");
        }

        private async Task DisconnectAsync()
        {
            await Clients.Client(Context.ConnectionId).SendAsync("disconnected");
        }
    }
}