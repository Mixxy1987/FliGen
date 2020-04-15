using FliGen.Services.Signalr.Framework;
using FliGen.Services.Signalr.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace FliGen.Services.Signalr.Services
{
    public class HubWrapper : IHubWrapper
    {
        private readonly IHubContext<FliGenHub> _hubContext;

        public HubWrapper(IHubContext<FliGenHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task PublishToUserAsync(Guid userId, string message, object data)
        {
            await _hubContext.Clients.Group(userId.ToUserGroup()).SendAsync(message, data);
        }

        public async Task PublishToAllAsync(string message, object data)
        {
            await _hubContext.Clients.All.SendAsync(message, data);
        }
    }
}